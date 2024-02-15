using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RoyalLifeSavings.Data;
using RoyalLifeSavings.Extensions;
using RoyalLifeSavings.Integrations;
using RoyalLifeSavings.Integrations.SendGrid;
using RoyalLifeSavings.Models;

namespace RoyalLifeSavings.Pages
{
    public class BuyRentModel : PageModel
    {
        private readonly RoyalLifeSavingDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IOptions<EmailOptions> _options;

        [BindProperty]
        public BuyOrRentModel Model { get; set; } = new();

        public BuyRentModel(RoyalLifeSavingDbContext context, IEmailSender emailSender, IOptions<EmailOptions> options)
        {
            _context = context;
            _emailSender = emailSender;
            _options = options;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            var emailData = Model.BuyOrRentEmailBody();
            await _emailSender.SendEmailAsync(_options.Value.ToEmail, emailData.Subject, emailData.Body);
            await _context.SaveChangesAsync();
            return this.RedirectToPage("ConfirmationForm");
        }
    }
}
