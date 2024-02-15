using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalLifeSavings.Integrations.VitalSource.Responses
{
    public class Fulfillment
    {
        public string code { get; set; }
        public string sku { get; set; }
        public Download_License download_license { get; set; }
        public Online_License online_license { get; set; }
        public string title { get; set; }
        public string vbid { get; set; }
        public string author { get; set; }
        public string imprint { get; set; }
        public string publisher { get; set; }
    }

    public class Download_License
    {
        public string details { get; set; }
        public string duration { get; set; }
    }

    public class Online_License
    {
        public string details { get; set; }
        public string duration { get; set; }
    }

}
