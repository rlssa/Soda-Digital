using System.ComponentModel.DataAnnotations;

namespace RoyalLifeSavings.Data
{
    public class EBook
    {
        /// <summary>
        /// Vital Book ID (VBID)
        /// </summary>
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Edition { get; set; }
        public string StripePriceId { get; set; }
        public ICollection<ApplicationUserEBook> UserLicences { get; set; } = new List<ApplicationUserEBook>();

        /// <summary>
        /// Wether this product should attract GST
        /// </summary>
        public bool TaxApplicable { get; set; }
    }
}
