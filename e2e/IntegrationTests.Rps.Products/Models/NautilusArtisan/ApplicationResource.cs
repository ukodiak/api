using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.NautilusArtisan
{
    [DataContract]
    public class ApplicationResource : Products.Models.ApplicationResource
    {
        [DataMember(Name = "productName")]
        public string ProductName { get; set; }

        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "hasPossibleOfacMatch")]
        public bool HasPossibleOfacMatch { get; set; }

        [DataMember(Name = "crafts")]
        public List<Craft> Crafts { get; set; }

        [DataMember(Name = "business")]
        public Business Business { get; set; }

        [DataMember(Name = "coverage")]
        public Coverage Coverage { get; set; }

        [DataMember(Name = "questionAnswers")]
        public List<QuestionAnswerData> QuestionAnswers { get; set; }

        [DataMember(Name = "additionalInsureds")]
        public List<AdditionalInsured> AdditionalInsureds { get; set; }

        [DataMember(Name = "additionalInsuredCoverageRequest")]
        public string AdditionalInsuredCoverageRequest { get; set; }

        [DataMember(Name = "statusChangeNotes")]
        public string StatusChangeNotes { get; set; }
    }
}