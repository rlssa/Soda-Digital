using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoyalLifeSavings.Data;
using RoyalLifeSavings.Integrations;
using RoyalLifeSavings.Integrations.SendGrid;

namespace RoyalLifeSavings.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public string Email { get; set; } = null!;

        public LoginModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();
            var user = await _userManager.FindByNameAsync(Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid Address");
                return Page();
            }

            var token = await _userManager.GenerateUserTokenAsync(user, "Default", "passwordless-auth");

            var url = Url.Action("LoginCallback", "Account", new { token = token, email = user.Email }, Request.Scheme);

            if (!string.IsNullOrEmpty(url))
            {
                await _emailSender.SendEmailAsync(user.Email, templateData: SendGridTemplateData.LoginLink(url));
            }

            return RedirectToPage("LoginSuccessful");
        }
    }
}
