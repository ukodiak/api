using System;
using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.NautilusArtisan
{
    [DataContract]
    public class Coverage
    {
        [DataMember(Name = "effective")]
        public DateTime? Effective { get; set; }

        [DataMember(Name = "expiration")]
        public DateTime? Expiration { get; set; }

        [DataMember(Name = "glAggregateLimit")]
        public int? GlAggregateLimit { get; set; }

        [DataMember(Name = "glDeductibleLimit")]
        public int? GlDeductibleLimit { get; set; }

        [DataMember(Name = "glPerOccurrenceLimit")]
        public int? GlPerOccurrenceLimit { get; set; }

        [DataMember(Name = "hasContractorExtensionCoverage")]
        public bool? HasContractorExtensionCoverage { get; set; }

        [DataMember(Name = "hasPesticideHerbicideCoverage")]
        public bool? HasPesticideHerbicideCoverage { get; set; }

        [DataMember(Name = "hasSwimmingPoolCoverage")]
        public bool? HasSwimmingPoolCoverage { get; set; }

        [DataMember(Name = "hasTerrorismCoverage")]
        public bool? HasTerrorismCoverage { get; set; }

        [DataMember(Name = "policyNumber")]
        public string PolicyNumber { get; set; }

        [DataMember(Name = "privacyBreachLimit")]
        public int? PrivacyBreachLimit { get; set; }

        [DataMember(Name = "stopGapLimit")]
        public int? StopGapLimit { get; set; }
    }
}