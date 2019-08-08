using IntegrationTests.Rps.Products.Models.NautilusArtisan;
using System;
using System.Collections.Generic;
using System.Configuration;
using ApplicationResource = IntegrationTests.Rps.Products.Models.NautilusArtisan.ApplicationResource;

namespace IntegrationTests.Rps.Products.Features.NautilusArtisan
{
    public class NautilusTestHelpers
    {
        private ArtisansApplicationStatus _currentAction = ArtisansApplicationStatus.None;
        private readonly string productId = ConfigurationManager.AppSettings["ArtisansProductId"];

        public ApplicationResource GetDefaultTestData(ArtisansApplicationStatus action)
        {
            var testAddress = new Models.AddressData()
            {
                AddressLineOne = "2600 Clifton Ave",
                AddressLineTwo = "",
                District = "Hamilton",
                Locality = "Cincinnati",
                Region = "OH",
                PostalCode = "45220"
            };

            var application = new ApplicationResource();

            while (_currentAction < action)
            {
                _currentAction++;

                if (_currentAction == ArtisansApplicationStatus.Open)
                {
                    var uniqueAppId = GetShortGuid();

                    var businessName = $"QA-SUCCESS-{uniqueAppId}";

                    application.Id = 1;
                    application.ProductId = Guid.Parse(productId);
                    application.Status = "open";
                    application.Business = new Business()
                    {
                        Name = businessName,
                        TotalGrossSales = 500000,
                        IsNewVenture = false,
                        IsLicensedAndBonded = true,
                        PriorLosses = new List<PriorLoss>()
                    };

                    application.Crafts = new List<Craft>
                    {
                        new Craft { Id = 112, ClassId = 90089, ClassCode = 90089, Percent = 100 }
                    };

                    application.AdditionalInsureds = new List<AdditionalInsured>();
                    application.Coverage = new Coverage();
                    application.QuestionAnswers = new List<QuestionAnswerData>();
                    application.ProducerId = 6;
                    application.ProducerOfficeId = 1;
                    application.ProductName = "Artisan Contractor Application";
                    application.HasPossibleOfacMatch = false;
                    application.UserId = 1;

                    application.QuestionAnswers = new List<QuestionAnswerData>()
                    {
                        new QuestionAnswerData { QuestionId = 25, Answer = false }, new QuestionAnswerData { QuestionId = 26, Answer = false },
                        new QuestionAnswerData { QuestionId = 27, Answer = false }, new QuestionAnswerData { QuestionId = 28, Answer = false },
                        new QuestionAnswerData { QuestionId = 29, Answer = false }, new QuestionAnswerData { QuestionId = 30, Answer = false },
                        new QuestionAnswerData { QuestionId = 31, Answer = false }, new QuestionAnswerData { QuestionId = 32, Answer = false },
                        new QuestionAnswerData { QuestionId = 33, Answer = false }, new QuestionAnswerData { QuestionId = 34, Answer = false },
                        new QuestionAnswerData { QuestionId = 35, Answer = false }, new QuestionAnswerData { QuestionId = 36, Answer = false },
                        new QuestionAnswerData { QuestionId = 37, Answer = false }, new QuestionAnswerData { QuestionId = 38, Answer = false },
                        new QuestionAnswerData { QuestionId = 39, Answer = false }, new QuestionAnswerData { QuestionId = 40, Answer = false },
                        new QuestionAnswerData { QuestionId = 41, Answer = false }, new QuestionAnswerData { QuestionId = 42, Answer = false },
                        new QuestionAnswerData { QuestionId = 43, Answer = false }, new QuestionAnswerData { QuestionId = 44, Answer = false },
                        new QuestionAnswerData { QuestionId = 72, Answer = false }
                    };

                    continue;
                }

                if (_currentAction == ArtisansApplicationStatus.Submitted)
                {
                    application.Coverage = new Coverage
                    {
                        Effective = DateTime.Now,
                        Expiration = DateTime.Now.AddYears(1)
                    };

                    application.Business.MailingAddress = testAddress;

                    application.Business.PhysicalAddress = testAddress;

                    application.Business.Email = $"Automation{GetShortGuid()}_@Automation.rpsins.com";
                    application.Business.PhoneNumber = "555-555-6666";
                    application.Business.NumberOfOwners = 1;
                    application.Business.EmployeePayroll = 5000;
                    application.Business.WorkSubcontracted = "ZeroPercent";
                    application.Business.ResidentialSubcontractorCost = 0.00M;
                    application.Business.CommercialSubcontractorCost = 0.00M;
                    application.Business.BusinessStructure = 1;
                    application.Business.NoCoverageInUnavailableRegion = true;

                    application.AdditionalInsureds = new List<AdditionalInsured>
                    {
                        new AdditionalInsured { Id = 0, Address = testAddress, FormNumber = "CG2010", Name = $"QA-SUCCESS-{GetShortGuid()}"}
                    };
                }

                if (_currentAction >= ArtisansApplicationStatus.Approved)
                {
                    application.AdditionalInsuredCoverageRequest = "Integration Testing - Additional Insured Coverage Request";
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

    public enum ArtisansApplicationStatus
    {
        None,
        Open,
        Submitted,
        Quoted,
        Bound,
        Approved,
        Declined
    }
}
