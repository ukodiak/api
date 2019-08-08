using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Nautilus.ArtisanContractor.Application;
using IntegrationTests.Rps.Products.Models.NautilusArtisan;
using System.Collections.Generic;
using NJsonSchema;
using System;
using System.Configuration;
using static Rps.Products.Api.Controllers.PaymentController;
using Rps.Products.Applications;

namespace IntegrationTests.Rps.Products.Features.NautilusArtisan
{
    [TestFixture]
    public class ApplicationControllerTestFixture
    {
        private HttpTestHelpers _httpHelpers;
        private NautilusTestHelpers _nautilusHelpers;
        private JsonSchema4 _schema;
        private readonly string _productId = ConfigurationManager.AppSettings["ArtisansProductId"];

        [SetUp]
        public async Task Setup()
        {
            _httpHelpers = new HttpTestHelpers();
            _nautilusHelpers = new NautilusTestHelpers();

            _schema = await _httpHelpers.GetJsonSchemaFromType<ArtisanApplicationResource>();
        }

        [TearDown]
        public async Task Cleanup()
        {
            await _httpHelpers.Cleanup();
        }

        [Test]
        public async Task Artisans_Get_ShouldReturnCorrectSchema()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Open);

            var createResult = await _httpHelpers.CreateApplication(testData);

            var applicationId = createResult.ApplicationId;

            var applicationResult = await _httpHelpers.GetApplication(applicationId);

            applicationResult.StatusCode.Should().Be(HttpStatusCode.OK);

            JObject appObj = JObject.Parse(applicationResult.Application);

            var schemaData = _schema.ToJson();
            var schemaErrors = _schema.Validate(appObj);

            schemaErrors.Count.Should().Be(0);

            var responseId = int.Parse(appObj.GetValue("id").ToString());

            responseId.Should().Be(applicationId);
        }        

        [Test]
        public async Task Artisans_Create_ShouldReturnCreatedStatus()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Open);

            var createResult = await _httpHelpers.CreateApplication(testData);

            createResult.StatusCode.Should().Be(HttpStatusCode.Created);                        
        }

        [Test]
        public async Task Artisans_Create_CraftsNot100Percent_ShouldReturnNotAcceptable()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Bound);

            testData.Crafts = new List<Craft>()
            {
                new Craft { Id = 112, ClassId = 90089, ClassCode = 90089, Percent = 50 },
                new Craft { Id = 113, ClassId = 91111, ClassCode = 91111, Percent = 45 }
            };

            var result = await _httpHelpers.CreateApplication(testData);

            result.StatusCode.Should().Be(HttpStatusCode.NotAcceptable);            
        }

        [Test]
        public async Task Artisans_Submit_ShouldReturnOkStatus()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Submitted);

            var result = await _httpHelpers.CreateApplication(testData);

            var submitStatus = await _httpHelpers.SubmitApplication(result.ApplicationId);

            submitStatus.Should().Be(HttpStatusCode.OK);            
        }

        [Test]
        public async Task Artisans_Approve_ShouldReturnOkStatus()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Approved);

            var result = await _httpHelpers.CreateApplication(testData);

            await _httpHelpers.SubmitApplication(result.ApplicationId);

            await _httpHelpers.CreateQuote(result.ApplicationId, true);

            var approveStatus = await _httpHelpers.CreateApprovedQuote(result.ApplicationId);

            approveStatus.Should().Be(HttpStatusCode.OK);            
        }

        [Test]
        public async Task Artisans_Extend_ShouldExtendEffectivePeriod()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Approved);

            var create = await _httpHelpers.CreateApplication(testData);
            var applicationId = create.ApplicationId;

            var backDateResult = await _httpHelpers.QaBackdate(_productId, applicationId);
            backDateResult.Should().Be(HttpStatusCode.OK);

            var initialEffectiveDate = await GetApplicationEffectiveDate(applicationId);
            var expectedEffectiveDate = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy");

            initialEffectiveDate.Should().Be(expectedEffectiveDate);

            var extendStatus = await _httpHelpers.ExtendQuote(applicationId);
            extendStatus.Should().Be(HttpStatusCode.OK);

            var extendedEffectiveDate = await GetApplicationEffectiveDate(applicationId);
            expectedEffectiveDate = DateTime.Now.ToString("MM/dd/yyyy");

            extendedEffectiveDate.Should().Be(expectedEffectiveDate);
        }

        [Test]
        public async Task Artisans_Decline_ShouldReturnOkStatus()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Declined);

            var result = await _httpHelpers.CreateApplication(testData);

            var declineReason = "Declined By Integration Test";

            var declineStatus = await _httpHelpers.DeclineQuote(result.ApplicationId, declineReason);

            declineStatus.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Artisans_Bind_ShouldCreateBoundQuote()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Bound);
            
            var result = await _httpHelpers.CreateApplication(testData);

            await _httpHelpers.CreateQuote(result.ApplicationId);
            await _httpHelpers.RequestBinder(result.ApplicationId);
            await _httpHelpers.RequestPayment(result.ApplicationId, new PaymentStatusRequest { PaymentStatus = PaymentStatus.Credit });
            var bindResult = await _httpHelpers.BindQuote(result.ApplicationId);

            bindResult.StatusCode.Should().Be(HttpStatusCode.OK);

            bindResult.PolicyNumber.Should().StartWith("QA");
        }

        [Test]
        public async Task Artisans_RequestBinder_WithDuplicateAdditionalInsureds_ShouldReturnNotAcceptable()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Bound);

            testData.AdditionalInsureds = new List<AdditionalInsured>()
            {
                new AdditionalInsured { Id = 0, Address = testData.Business.PhysicalAddress, FormNumber = "CG2010", Name = "Additional Insured 1" },
                new AdditionalInsured { Id = 0, Address = testData.Business.PhysicalAddress, FormNumber = "CG2010", Name = "Additional Insured 1" }
            };

            var result = await _httpHelpers.CreateApplication(testData);

            await _httpHelpers.CreateQuote(result.ApplicationId);
            var requestBinderResult = await _httpHelpers.RequestBinder(result.ApplicationId);

            requestBinderResult.Should().Be(HttpStatusCode.NotAcceptable);
        }

        [Test]
        public async Task Artisans_RequestBinder_WithUniqueAdditionalInsureds_ShouldReturnOk()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Bound);

            testData.AdditionalInsureds = new List<AdditionalInsured>()
            {
                new AdditionalInsured { Id = 0, Address = testData.Business.PhysicalAddress, FormNumber = "CG2010", Name = "Additional Insured 1" },
                new AdditionalInsured { Id = 0, Address = testData.Business.PhysicalAddress, FormNumber = "CG2010", Name = "Additional Insured 2" }
            };
            var result = await _httpHelpers.CreateApplication(testData);

            await _httpHelpers.CreateQuote(result.ApplicationId);
            var requestBinderResult = await _httpHelpers.RequestBinder(result.ApplicationId);

            requestBinderResult.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task Artisans_RequestBinder_NoAdditionalInsuredDetail_ShouldReturnInvalid()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Bound);

            testData.AdditionalInsureds = new List<AdditionalInsured>()
            {
                new AdditionalInsured { Id = 0, Address = null, FormNumber = "CG2010", Name = string.Empty },
            };

            var result = await _httpHelpers.CreateApplication(testData);
            var requestBinderResult = await _httpHelpers.RequestBinder(result.ApplicationId);

            requestBinderResult.Should().Be(HttpStatusCode.NotAcceptable);
        }

        [Test]
        public async Task Artisans_RequestBinder_TotalSubcontractorCostEqualZero_ShouldReturnNotAcceptable()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Bound);

            testData.Business.WorkSubcontracted = "TenPercent";
            testData.Business.CommercialSubcontractorCost = 0.00M;
            testData.Business.ResidentialSubcontractorCost = 0.00M;

            var result = await _httpHelpers.CreateApplication(testData);

            var binderRequestResult = await _httpHelpers.RequestBinder(result.ApplicationId);

            binderRequestResult.Should().Be(HttpStatusCode.NotAcceptable);
        } 
        
        private async Task<string> GetApplicationEffectiveDate(int applicationId)
        {
            var applicationResult = await _httpHelpers.GetApplication(applicationId);

            JObject appObj = JObject.Parse(applicationResult.Application);

            var effectiveDate = DateTime.Parse(appObj.SelectToken("coverage.effective").ToString());

            return effectiveDate.Date.ToString("MM/dd/yyyy");
        }
    }
}