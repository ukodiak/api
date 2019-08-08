using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.ChubbBop
{
    [DataContract]
    public class AdditionalInsuredData
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "address")]
        public AddressData Address { get; set; }
    }
}