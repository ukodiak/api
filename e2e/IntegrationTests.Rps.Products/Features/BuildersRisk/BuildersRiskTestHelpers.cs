using IntegrationTests.Rps.Products.Models;
using System;
using System.Configuration;
using IntegrationTests.Rps.Products.Models.BuildersRisk;
using ApplicationResource = IntegrationTests.Rps.Products.Models.BuildersRisk.ApplicationResource;
using IntegrationTests.Rps.Products.Properties;

namespace IntegrationTests.Rps.Products.Features.BuildersRisk
{
    public class BuildersRiskTestHelpers
    {
        private BuildersRiskApplicationStatus _currentStatus = BuildersRiskApplicationStatus.None;
        private readonly string productId = ConfigurationManager.AppSettings["BuildersRiskProductId"];

        public ApplicationResource GetDefaultTestData(BuildersRiskApplicationStatus status)
        {
            var testAddress = new AddressData()
            {
                AddressLineOne = "2600 Clifton Ave",
                AddressLineTwo = "",
                District = "Hamilton",
                Locality = "Cincinnati",
                Region = "OH",
                PostalCode = "45220"
            };

            var application = new ApplicationResource();

            while (_currentStatus < status)
            {
                _currentStatus++;
                var uniqueAppId = GetShortGuid();

                if (_currentStatus == BuildersRiskApplicationStatus.Open)
                {
                    var businessName = $"QA-SUCCESS-{uniqueAppId}";

                    application.Id = 1;
                    application.ProductId = Guid.Parse(productId);
                    application.Status = "open";

                    application.Coverage = new CoverageData();
                    application.ProducerId = 1;
                    application.ProducerOfficeId = 1;
                    application.ProductName = "Builders Risk";
                    application.HasPossibleOfacMatch = false;
                    application.UserId = 1;

                    application.Applicant = new ApplicantData { Name = businessName };

                    continue;
                }

                if (_currentStatus == BuildersRiskApplicationStatus.Submitted)
                {
                    application.Applicant.MailingAddress = testAddress;
                    application.Project = new ProjectData { Description = $"Project-{uniqueAppId}", Location = testAddress };
                    application.Coverage = new CoverageData
                    {
                        Effective = DateTime.Now.AddDays(1),
                        Expiration = DateTime.Now.AddDays(1).AddYears(1),
                        HardCostLimit = 25000000,
                        SoftCostLimit = 1000,
                        IncomeLossLimit = 1000,
                        ReduceDeductibles = false
                    };

                    continue;
                }

            }

            return application;
        }

        public AttachmentResource CreateAttachment(string category)
        {
            return new AttachmentResource
            {
                Category = category,
                ContentType = "application/pdf",
                Name = category + GetShortGuid() + ".pdf",
                Data = Resources.BuildersRiskAttachment
            };
        }

        private string GetShortGuid()
        {
            var guid = Guid.NewGuid();
            string encoded = Convert.ToBase64String(guid.ToByteArray());

            encoded = encoded.Replace("/", string.Empty);
            encoded = encoded.Replace("+", string.Empty);
            encoded = encoded.Replace("=", string.Empty);

            return encoded;
        }
    }

    public enum BuildersRiskApplicationStatus
    {
        None,
        Open,
        Submitted        
    }
}
