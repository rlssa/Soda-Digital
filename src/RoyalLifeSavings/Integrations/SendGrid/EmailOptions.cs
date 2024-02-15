using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalLifeSavings.Integrations.SendGrid
{
    public class EmailOptions
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ToEmail { get; set; }
        public string TemplateId { get; set; }
    }
}
