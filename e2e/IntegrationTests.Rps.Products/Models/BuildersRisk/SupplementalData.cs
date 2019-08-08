using System.Runtime.Serialization;

namespace IntegrationTests.Rps.Products.Models.BuildersRisk
{
    public class SupplementalData
    {
        [DataMember(Name = "insuredIsContractor")]
        public bool InsuredIsContractor { get; set; }

        [DataMember(Name = "contractorName")]
        public string ContractorName { get; set; }

        [DataMember(Name = "contractorYearsInBusiness")]
        public int ContractorYearsInBusiness { get; set; }

        [DataMember(Name = "contractorAddress")]
        public AddressData ContractorAddress { get; set; }

        [DataMember(Name = "propertyIsInCityLimits")]
        public bool PropertyIsInCityLimits { get; set; }

        [DataMember(Name = "distanceToHydrant")]
        public int DistanceToHydrant { get; set; }

        [DataMember(Name = "distanceToFireDepartment")]
        public decimal DistanceToFireDepartment { get; set; }

        [DataMember(Name = "distanceFromCoastalWaters")]
        public decimal DistanceFromCoastalWaters { get; set; }

        [DataMember(Name = "totalSquareFootage")]
        public int TotalSquareFootage { get; set; }

        [DataMember(Name = "numberOfBuildings")]
        public int NumberOfBuildings { get; set; }

        [DataMember(Name = "distanceBetweenBuildings")]
        public int DistanceBetweenBuildings { get; set; }

        [DataMember(Name = "numberOfStories")]
        public int NumberOfStories { get; set; }

        [DataMember(Name = "numberOfUnits")]
        public int NumberOfUnits { get; set; }

        [DataMember(Name = "intendedOccupancy")]
        public string IntendedOccupancy { get; set; }

        [DataMember(Name = "lossControlContactName")]
        public string LossControlContactName { get; set; }

        [DataMember(Name = "lossControlContactInfo")]
        public string LossControlContactInfo { get; set; }

        [DataMember(Name = "agentContactNumber")]
        public string AgentContactNumber { get; set; }
    }
}