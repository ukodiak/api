using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.NautilusArtisan
{
    [DataContract]
    public class Craft
    {
        [DataMember(Name = "categories")]
        public List<string> Categories { get; set; }

        [DataMember(Name = "classCode")]
        public int ClassCode { get; set; }

        [DataMember(Name = "classId")]
        public int ClassId { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "percent")]
        public int Percent { get; set; }
    }
}