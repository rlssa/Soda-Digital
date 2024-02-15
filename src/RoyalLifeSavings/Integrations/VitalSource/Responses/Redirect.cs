using System.Xml.Serialization;

namespace RoyalLifeSavings.Integrations.VitalSource.Responses
{
    [XmlRoot(ElementName = "redirect")]
    public class Redirect
    {

        [XmlAttribute(AttributeName = "auto-signin")]
        public string AutoSignin { get; set; }

        [XmlAttribute(AttributeName = "expires")]
        public string Expires { get; set; }
    }
}


