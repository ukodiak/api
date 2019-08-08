using IntegrationTests.Rps.Products.Models;
using IntegrationTests.Rps.Products.Models.ChubbBop;
using System;
using System.Collections.Generic;
using System.Configuration;
using ApplicationResource = IntegrationTests.Rps.Products.Models.ChubbBop.ApplicationResource;

namespace IntegrationTests.Rps.Products.Features.ChubbBop
{
    public class ChubbBopTestHelpers
    {
        private ChubbApplicationStatus _currentStatus = ChubbApplicationStatus.None;
        private readonly string productId = ConfigurationManager.AppSettings["ChubbProductId"];

        public ApplicationResource GetDefaultTestData(ChubbApplicationStatus status)
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

                if (_currentStatus == ChubbApplicationStatus.Open)
                {
                    var businessName = $"QA-SUCCESS-{uniqueAppId}";

                    application.Id = 1;
                    application.ProductId = Guid.Parse(productId);
                    application.Status = "open";

                    application.Business = new BusinessData { Name = businessName, MailingAddress = testAddress, PhysicalAddress = testAddress };

                    application.AdditionalInsured = new AdditionalInsuredData();
                    application.Coverage = new CoverageData();
                    application.ProducerId = 6;
                    application.ProducerOfficeId = 1;
                    application.ProductName = "Chubb Bop";
                    application.HasPossibleOfacMatch = false;
                    application.UserId = 1;
                    application.Specialty = new SpecialtyResource { Id = 1, Name = "", Exposure = ExposureType.Area };

                    application.QuestionAnswers = new List<QuestionAnswerData>()
                    {
                        new QuestionAnswerData { QuestionId = 1, Answer = false },
                        new QuestionAnswerData { QuestionId = 18, Answer = false },
                        new QuestionAnswerData { QuestionId = 19, Answer = false },
                        new QuestionAnswerData { QuestionId = 23, Answer = true },
                        new QuestionAnswerData { QuestionId = 24, Answer = false }
                    };

                    continue;
                }

                if (_currentStatus == ChubbApplicationStatus.Quoted)
                {                    
                    application.Business.Phone = "555-555-5555";
                    application.Business.Email = $"Automation_{uniqueAppId}@Automation.rpsins.com";
                    application.Business.EntityType = 1;
                    application.Business.YearFounded = 2015;
                    application.Business.SquareFootage = 2000;

                    application.Coverage.Effective = DateTime.Now.AddDays(1);
                    application.Coverage.Expiration = DateTime.Now.AddDays(1).AddYears(1);
                    application.Coverage.HasTerrorismCoverage = true;

                    application.AdditionalInsured = new AdditionalInsuredData { Name = "ai", Address = testAddress };

                    continue;
                }
            }

            return application;
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

    public enum ChubbApplicationStatus
    {
        None,
        Open,        
        Quoted,
        Bound
    }
}
