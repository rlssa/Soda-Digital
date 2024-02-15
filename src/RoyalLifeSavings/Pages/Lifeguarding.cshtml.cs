using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoyalLifeSavings.Data;
using Stripe;
using Stripe.Checkout;

namespace RoyalLifeSavings.Pages
{
    public class LifeguardingModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IStripeClient _stripe;
        private readonly RoyalLifeSavingDbContext _db;
        private readonly IConfiguration _configuration;

        public LifeguardingModel(ILogger<IndexModel> logger, IStripeClient stripe, RoyalLifeSavingDbContext db, IConfiguration configuration)
        {
            _logger = logger;
            _stripe = stripe;
            _db = db;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnPost()
        {
            var book = await _db.EBooks.FindAsync("VCS0429073907956");
            if (book == null) return Page();
            var domain = Request.Scheme + "://" + Request.Host.ToUriComponent();

            var stripeTaxId = _configuration.GetValue<string>("Stripe:GSTTaxRateId");


            var lineItem = new SessionLineItemOptions
            {
                // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                Price = book.StripePriceId,
                Quantity = 1
            };

            if (book.TaxApplicable)
            {
                //we want to edit the line item and add tax
                lineItem.TaxRates = new List<string>
                {
                    stripeTaxId
                };
            }


            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    lineItem
                },
                Mode = "payment",
                InvoiceCreation = new SessionInvoiceCreationOptions { Enabled = true },
                SuccessUrl = domain + "/Confirmation?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/Index",
                Metadata = new Dictionary<string, string>() { { "VitalSourceEBookId", book.Id } }
            };



            var service = new SessionService(_stripe);
            var session = await service.CreateAsync(options);

            return Redirect(session.Url);
        }
    }
}
