using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RoyalLifeSavings.Data;
using RoyalLifeSavings.Integrations;
using RoyalLifeSavings.Integrations.SendGrid;
using RoyalLifeSavings.Integrations.VitalSource;
using Stripe;
using Stripe.Checkout;

namespace RoyalLifeSavings.Pages
{
    public class ConfirmationModel : PageModel
    {
        private readonly VitalSourceWorkflow _vitalSourceWorkflow;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly RoyalLifeSavingDbContext _db;
        private readonly IStripeClient _stripe;
        public EBookViewModel Ebook { get; set; } = null!;

        public ConfirmationModel(IStripeClient stripe, VitalSourceWorkflow vitalSourceWorkflow,
            UserManager<ApplicationUser> userManager, IEmailSender emailSender, RoyalLifeSavingDbContext db)
        {
            _vitalSourceWorkflow = vitalSourceWorkflow;
            _userManager = userManager;
            _emailSender = emailSender;
            _db = db;
            _emailSender = emailSender;
            _stripe = stripe;
        }

        public async Task OnGet(string session_id)
        {
            if (string.IsNullOrEmpty(session_id)) return;
            var service = new SessionService(_stripe);
            var options = new SessionGetOptions();
            options.AddExpand("line_items");
            var session = await service.GetAsync(session_id, options);
            var priceId = session.LineItems.First().Price.Id;
            var email = session.CustomerDetails.Email;
            if (string.IsNullOrWhiteSpace(email)) return;
            var user = await _userManager.FindByEmailAsync(email);
            var ebook = await _db.EBooks.FindAsync(session.Metadata["VitalSourceEBookId"]);
            var orderPlaced = false;
            Ebook = new EBookViewModel
            {
                Name = ebook?.Name, Price = (session.LineItems.First().Price.UnitAmountDecimal ?? 0) / 100
            };
            if (user == null)
            {
                await _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    Orders = new List<Order>()
                    {
                        new Order
                        {
                            StripeSessionId = session_id,
                            CreatedAt = DateTimeOffset.Now,
                            ModifiedAt = DateTimeOffset.Now,
                            EbookId = ebook?.Id,
                            StripePriceId = priceId,
                            StripePaymentStatus = session.PaymentStatus
                        }
                    }
                });
            }
            else
            {
                orderPlaced = await _db.Orders.AnyAsync(x => x.StripeSessionId == session_id && x.UserId == user.Id);
                if (!orderPlaced)
                {
                    user.Orders.Add(new Order
                    {
                        StripeSessionId = session_id,
                        CreatedAt = DateTimeOffset.Now,
                        ModifiedAt = DateTimeOffset.Now,
                        EbookId = ebook?.Id,
                        StripePriceId = priceId,
                        StripePaymentStatus = session.PaymentStatus
                    });
                }

                await _userManager.UpdateAsync(user);
            }

            if (session.PaymentStatus == "paid")
            {
                await _vitalSourceWorkflow.RunWorkflowAsync(email, ebook?.Id);
                user = await _userManager.FindByEmailAsync(email);

                if (user is not null && !orderPlaced)
                {
                    var token = await _userManager.GenerateUserTokenAsync(user, "Default", "passwordless-auth");
                    var url = Url.Action("LoginCallback", "Account", new { token = token, email = email },
                        Request.Scheme);

                    if (!string.IsNullOrEmpty(url))
                    {
                        await _emailSender.SendEmailAsync(user.Email, templateData: SendGridTemplateData.LoginLink(url));
                    }
                }
            }
        }
    }

    public class EBookViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
