using System.Xml.Serialization;

namespace RoyalLifeSavings.Integrations.VitalSource.Responses
{
    [XmlRoot(ElementName = "user")]
    public class UserDetails
    {

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "first-name")]
        public string Firstname { get; set; }

        [XmlElement(ElementName = "last-name")]
        public string Lastname { get; set; }

        [XmlElement(ElementName = "reference")]
        public string Reference { get; set; }

        [XmlElement(ElementName = "sso-only")]
        public bool Ssoonly { get; set; }

        [XmlElement(ElementName = "require-deactivate")]
        public bool Requiredeactivate { get; set; }

        [XmlElement(ElementName = "require-book-list-update")]
        public bool Requirebooklistupdate { get; set; }

        [XmlElement(ElementName = "store-url")]
        public object Storeurl { get; set; }

        [XmlElement(ElementName = "timestamp")]
        public double Timestamp { get; set; }

        [XmlElement(ElementName = "max-desktop-activations")]
        public int Maxdesktopactivations { get; set; }

        [XmlElement(ElementName = "max-mobile-activations")]
        public int Maxmobileactivations { get; set; }

        [XmlElement(ElementName = "email-locked")]
        public bool Emaillocked { get; set; }
    }
}
