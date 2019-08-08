using IntegrationTests.Rps.Products.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.NautilusArtisan
{
    [DataContract]
    public class Business
    {
        [DataMember(Name = "businessStructure")]
        public int BusinessStructure { get; set; }

        [DataMember(Name = "commercialSubcontractorCost")]
        public decimal CommercialSubcontractorCost { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "employeePayroll")]
        public decimal EmployeePayroll { get; set; }

        [DataMember(Name = "hasTooManyLosses")]
        public bool HasTooManyLosses { get; set; }

        [DataMember(Name = "isLicensedAndBonded")]
        public bool IsLicensedAndBonded { get; set; }

        [DataMember(Name = "isNewVenture")]
        public bool IsNewVenture { get; set; }

        [DataMember(Name = "hasCoverageLapse")]
        public bool? HasCoverageLapse { get; set; }

        [DataMember(Name = "noCoverageInUnavailableRegion")]
        public bool? NoCoverageInUnavailableRegion { get; set; }

        [DataMember(Name = "mailingAddress")]
        public AddressData MailingAddress { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "numberOfOwners")]
        public int NumberOfOwners { get; set; }

        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "physicalAddress")]
        public AddressData PhysicalAddress { get; set; }

        [DataMember(Name = "priorLosses")]
        public List<PriorLoss> PriorLosses { get; set; }

        [DataMember(Name = "residentialSubcontractorCost")]
        public decimal ResidentialSubcontractorCost { get; set; }

        [DataMember(Name = "totalGrossSales")]
        public decimal TotalGrossSales { get; set; }

        [DataMember(Name = "workSubcontracted")]
        public string WorkSubcontracted { get; set; }
    }
}