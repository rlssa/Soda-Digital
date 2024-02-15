using System.Xml.Serialization;

namespace RoyalLifeSavings.Integrations.VitalSource.Responses
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    public partial class UserDetailsFull
    {

        [XmlElement("email")]
        public string Email { get; set; }

        /// <remarks/>
        [XmlElement("first-name")]
        public string FirstName { get; set; }
        

        /// <remarks/>
        [XmlElement("last-name")]
        public string LastName { get; set; }
        
        /// <remarks/>
        public string Reference { get; set; }
        
        /// <remarks/>
        [XmlElement("sso-only")]
        public bool SsoOnly { get; set; }
        

        /// <remarks/>
        [XmlElement("require-deactivate")]
        public bool RequireDeactivate { get; set; }
        
        /// <remarks/>
        [XmlElement("require-book-list-update")]
        public bool RequireBookListUpdate { get; set; }
        

        /// <remarks/>
        [XmlElement("store-url")]
        public object StoreUrl { get; set; }
        
        /// <remarks/>
        public ulong timestamp
        {
            get; set;
        }

        /// <remarks/>
        [XmlElement("max-desktop-activations")]
        public int MaxDesktopActivations { get; set; }

        /// <remarks/>
        [XmlElement("max-mobile-activations")]
        public byte maxmobileactivations
        {
            get; set;
        }

        /// <remarks/>
        [XmlElement("email-locked")]
        public bool emaillocked
        {
            get; set;
        }

        /// <remarks/>
        [XmlElement("strong-password")]
        public bool strongpassword
        {
            get; set;
        }

        /// <remarks/>
        [XmlElement("password-updated")]
        public string PasswordUpdated { get; set; }
        

        /// <remarks/>
        public object locale { get; set; }
        
        /// <remarks/>
        [XmlElement("referral-guid")]
        public string ReferralGuid { get; set; }
        
        /// <remarks/>
        [XmlElement("middle-initial")]
        public object MiddleInitial { get; set; }
        

        /// <remarks/>
        public object country { get; set; }
        

        /// <remarks/>
        public object state { get; set; }
        

        /// <remarks/>
        public object affiliate { get; set; }
        
        /// <remarks/>
        [XmlElement("promote-option")]
        public bool promoteoption { get; set; }

        /// <remarks/>
        [XmlElement("survey-option")]
        public bool surveyoption { get; set; }

        /// <remarks/>
        public bool eula_accepted { get; set; }

        /// <remarks/>
        public string eula_accepted_at { get; set; }
        
        /// <remarks/>
        public object eula_type { get; set; }
        

        /// <remarks/>
        [XmlElement("question-response")]
        public string QuestionResponse { get; set; }
        

        /// <remarks/>
        [XmlElement("question-id")]
        public int QuestionId { get; set; }
        

        /// <remarks/>
        [XmlElement("last-brand-accessed")]
        public string LastBrandAccessed { get; set; }
        
        
        /// <remarks/>
        [XmlElement("bookshelf-preference")]
        public string BookshelfPreference { get; set; }
        
        /// <remarks/>
        [XmlElement("save-last-cc")]
        public bool SaveLastCc { get; set; }
        
        /// <remarks/>
        public bool capture_analytics { get; set; }
        

        /// <remarks/>
        public bool disabled { get; set; }
        
        /// <remarks/>
        public bool email_verification_required { get; set; }
        
        /// <remarks/>
        public bool email_verification_completed { get; set; }
        
        /// <remarks/>
        public object third_party_sharing { get; set; }
        

        /// <remarks/>
        public object roles { get; set; }
        
    }

}
