using IntegrationTests.Rps.Products.Models;
using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.NautilusArtisan
{
    [DataContract]
    public class AdditionalInsured
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "formNumber")]
        public string FormNumber { get; set; }

        [DataMember(Name = "address")]
        public AddressData Address { get; set; }
    }
}