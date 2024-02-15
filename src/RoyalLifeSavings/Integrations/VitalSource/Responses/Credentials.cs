using System.Xml.Serialization;

namespace RoyalLifeSavings.Integrations.VitalSource.Responses
{

    [XmlRoot(ElementName = "credential")]
    public class Credential
    {
        [XmlAttribute(AttributeName = "email")]
        public string Email { get; set; }

        [XmlAttribute(AttributeName = "access-token")]
        public string AccessToken { get; set; }

        [XmlAttribute(AttributeName = "guid")]
        public string Guid { get; set; }

        [XmlAttribute(AttributeName = "reference")]
        public string Reference { get; set; }

        [XmlAttribute(AttributeName = "email_verification_required")]
        public bool EmailVerificationRequired { get; set; }

        [XmlAttribute(AttributeName = "email_verification_completed")]
        public bool EmailVerificationCompleted { get; set; }
    }

    [XmlRoot(ElementName = "credentials")]
    public class Credentials
    {

        [XmlElement(ElementName = "credential")]
        public Credential Credential { get; set; }
    }
}
