using System;
using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.ChubbBop
{
    [DataContract]
    public class CoverageData
    {
        [DataMember(Name = "policyNumber")]
        public string PolicyNumber { get; set; }

        [DataMember(Name = "isPolicyCancelled")]
        public bool IsPolicyCancelled { get; set; }

        [DataMember(Name = "effective")]
        public DateTime? Effective { get; set; }

        [DataMember(Name = "expiration")]
        public DateTime? Expiration { get; set; }

        [DataMember(Name = "liabilityLimit")]
        public int LiabilityLimit { get; set; }

        [DataMember(Name = "liabilityPerOccurrenceLimit")]
        public int LiabilityPerOccurrenceLimit { get; set; }

        [DataMember(Name = "businessPropertyLimit")]
        public int? BusinessPropertyLimit { get; set; }

        [DataMember(Name = "hasTerrorismCoverage")]
        public bool HasTerrorismCoverage { get; set; }
    }
}