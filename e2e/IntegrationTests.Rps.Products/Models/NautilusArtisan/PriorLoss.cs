using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.NautilusArtisan
{
    [DataContract]
    public class PriorLoss
    {
        [DataMember(Name = "amountPaid")]
        public int AmountPaid { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "isOpen")]
        public bool IsOpen { get; set; }

        [DataMember(Name = "lossType")]
        public string LossType { get; set; }
    }
}