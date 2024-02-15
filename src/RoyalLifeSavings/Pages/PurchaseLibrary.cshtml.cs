using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RoyalLifeSavings.Data;
using RoyalLifeSavings.Integrations.VitalSource;
using static RoyalLifeSavings.Services.Policies;

namespace RoyalLifeSavings.Pages
{
    public class PurchaseLibraryModel : PageModel
    {
        private readonly RoyalLifeSavingDbContext _db;
        private readonly VitalSourceService _vitalSource;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly ILogger<PurchaseLibraryModel> _logger;

        [BindProperty]
        public string BookId { get; set; } = string.Empty;
        public List<EbookLineItem> Ebooks { get; set; } = new();
        public PurchaseLibraryModel(RoyalLifeSavingDbContext db, VitalSourceService vitalSource, UserManager<ApplicationUser> userManger, ILogger<PurchaseLibraryModel> logger)
        {
            _db = db;
            _vitalSource = vitalSource;
            _userManger = userManger;
            _logger = logger;
        }

        public async Task OnGet()
        {
            var user = await _userManger.GetUserAsync(User);
            if (user is not null)
            {
                var ebooks = await _db.ApplicationUserEBooks
                    .Include(x => x.EBook)
                    .Where(x => x.UserId == user.Id)
                    .Where(x => x.EBook != null)
                    .ToListAsync();

                Ebooks = ebooks.GroupBy(x => x.EBookId).Select(x => new EbookLineItem { Id = x.First().Id, Name = x.First().EBook!.Name, VitalSourceId = x.Key ?? string.Empty, Quantity = x.Count() }).ToList();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManger.GetUserAsync(User);
            if (!string.IsNullOrEmpty(user.VitalSourceReferenceString))
            {
                try
                {
                    var accessToken = await Retry.ExecuteAsync(async () => await _vitalSource.VerifyUserAsync(user));
                    var redirect = await Retry.ExecuteAsync(async () => await _vitalSource.GetOnlineAccessAsync(accessToken, BookId));
                    if (!string.IsNullOrEmpty(redirect))
                    {
                        return Redirect(redirect);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Link to the e-book couldn't be generated");
                    ModelState.AddModelError("", "Link to the e-book couldn't be generated");
                }
            }

            return Page();
        }

        public class EbookLineItem
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = null!;
            public string VitalSourceId { get; set; } = null!;
            public int Quantity { get; set; }

        }
    }
}
