using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models
{
    [DataContract]
    public class QuestionResource
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "declinationAnswer")]
        public bool? DeclinationAnswer { get; set; }
    }
}
