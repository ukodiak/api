using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.BuildersRisk
{
    public class ApplicantData
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "mailingAddress")]
        public AddressData MailingAddress { get; set; }
    }
}