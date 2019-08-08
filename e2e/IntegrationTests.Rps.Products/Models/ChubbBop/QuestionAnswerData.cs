using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.ChubbBop
{
    [DataContract]
    public class QuestionAnswerData
    {
        [DataMember(Name = "questionId")]
        public int QuestionId { get; set; }

        [DataMember(Name = "answer")]
        public bool? Answer { get; set; }
    }
}