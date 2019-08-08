using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Models.ChubbBop
{
    [DataContract]
    public class ApplicationResource : Models.ApplicationResource
    {
        [DataMember(Name = "specialty")]
        public SpecialtyResource Specialty { get; set; }

        [DataMember(Name = "business")]
        public BusinessData Business { get; set; }

        [DataMember(Name = "additionalInsured")]
        public AdditionalInsuredData AdditionalInsured { get; set; }

        [DataMember(Name = "questionAnswers")]
        public List<QuestionAnswerData> QuestionAnswers { get; set; }

        [DataMember(Name = "coverage")]
        public CoverageData Coverage { get; set; }

        [DataMember(Name = "productName")]
        public string ProductName { get; set; }

        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "hasPossibleOfacMatch")]
        public bool HasPossibleOfacMatch { get; set; }
    }
}
