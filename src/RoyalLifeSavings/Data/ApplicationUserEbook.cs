using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalLifeSavings.Data
{
    /// <summary>
    /// User licence 
    /// </summary>
    public class ApplicationUserEBook
    {
        public Guid Id { get; set; }
        public EBook EBook { get; set; }
        public string EBookId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid? UserId { get; set; }
        public bool FulfillmentAdded { get; set; }
        public string VitalSourceFulfillmetCode { get; set; }
    }
}
