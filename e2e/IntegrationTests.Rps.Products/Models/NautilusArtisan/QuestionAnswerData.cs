using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.NautilusArtisan
{
    [DataContract]
    public class QuestionAnswerData
    {
        [DataMember(Name = "answer")]
        public bool? Answer { get; set; }

        [DataMember(Name = "questionId")]
        public int QuestionId { get; set; }
    }
}