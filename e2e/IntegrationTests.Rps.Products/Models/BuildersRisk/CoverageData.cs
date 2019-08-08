using System;
using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.BuildersRisk
{
    public class CoverageData
    {
        [DataMember(Name = "effective")]
        public DateTime? Effective { get; set; }

        [DataMember(Name = "expiration")]
        public DateTime? Expiration { get; set; }

        [DataMember(Name = "hardCostLimit")]
        public int HardCostLimit { get; set; }

        [DataMember(Name = "softCostLimit")]
        public int SoftCostLimit { get; set; }

        [DataMember(Name = "incomeLossLimit")]
        public int IncomeLossLimit { get; set; }

        [DataMember(Name = "reduceDeductibles")]
        public bool ReduceDeductibles { get; set; }
    }
}