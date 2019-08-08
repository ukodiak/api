using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models
{
    public class AttachmentResource
    {        
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }

        [DataMember(Name = "category")]
        public string Category { get; set; }

        [DataMember(Name = "data")]
        public byte[] Data { get; set; }
    }
}