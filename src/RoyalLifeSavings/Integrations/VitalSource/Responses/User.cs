using System.Xml.Serialization;

namespace RoyalLifeSavings.Integrations.VitalSource.Responses
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [XmlRoot(ElementName = "user")]
    public class User
    {
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "first-name")]
        public string Firstname { get; set; }

        [XmlElement(ElementName = "last-name")]
        public string Lastname { get; set; }

        [XmlElement(ElementName = "guid")]
        public string Guid { get; set; }

        [XmlElement(ElementName = "access-token")]
        public string Accesstoken { get; set; }

        [XmlElement(ElementName = "library")]
        public object Library { get; set; }
    }
}
