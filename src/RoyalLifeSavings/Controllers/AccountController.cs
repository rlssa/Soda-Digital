using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoyalLifeSavings.Data;

namespace RoyalLifeSavings.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("LoginCallback")]
        public async Task<IActionResult> LoginCallback(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound();

            var isValid = await _userManager.VerifyUserTokenAsync(user, "Default", "passwordless-auth", token);

            if (isValid)
            {
                // prevent the same token being used again 
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignInAsync(user, false);

                return RedirectToPage("/PurchaseLibrary");
            }
            return RedirectToPage("/Login");
        }
    }
}
