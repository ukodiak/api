using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.BuildersRisk
{
    public class ProjectData
    {
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name ="location")]
        public AddressData Location { get; set; }
    }
}