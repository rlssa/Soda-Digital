using Microsoft.AspNetCore.Identity;

namespace RoyalLifeSavings.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string VitalSourceReferenceString { get; set; }
        public string VitalSourceAccessToken { get; set; }
        public ICollection<ApplicationUserEBook> UserLicences { get; set; } = new List<ApplicationUserEBook>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
