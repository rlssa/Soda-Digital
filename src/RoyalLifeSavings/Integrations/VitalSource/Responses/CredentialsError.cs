using System.Xml.Serialization;

namespace RoyalLifeSavings.Integrations.VitalSource.Responses
{

    [XmlRoot(ElementName = "error")]
    public class Error
    {
        [XmlAttribute(AttributeName = "code")]
        public int Code { get; set; }

        [XmlAttribute(AttributeName = "email")]
        public object Email { get; set; }

        [XmlAttribute(AttributeName = "message")]
        public string Message { get; set; }

        [XmlAttribute(AttributeName = "guid")]
        public object Guid { get; set; }

        [XmlAttribute(AttributeName = "reference")]
        public object Reference { get; set; }
    }

    [XmlRoot(ElementName = "credentials")]
    public class CredentialsError
    {

        [XmlElement(ElementName = "error")]
        public Error Error { get; set; }
    }
}
