using FluentAssertions;
using IntegrationTests.Rps.Products.Models.BuildersRisk;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using NUnit.Framework;
using SanFrancisco.Tru.BuildersRisk.Application;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Features.BuildersRisk
{
    [TestFixture]
    public class ApplicationServiceTestFixture
    {
        private HttpTestHelpers _httpHelpers;
        private BuildersRiskTestHelpers _buildersRiskHelpers;
        private JsonSchema4 _schema;
        private readonly string _productId = ConfigurationManager.AppSettings["BuildersRiskProductId"];

        [SetUp]
        public async Task Setup()
        {
            _httpHelpers = new HttpTestHelpers();
            _buildersRiskHelpers = new BuildersRiskTestHelpers();
            _schema = await _httpHelpers.GetJsonSchemaFromType<SfbrApplicationResource>();
        }

        [TearDown]
        public async Task Cleanup()
        {
            await _httpHelpers.Cleanup();
        }

        [Test]
        public async Task BuildersRisk_Create_ShouldReturnCreatedStatus()
        {
            var testData = _buildersRiskHelpers.GetDefaultTestData(BuildersRiskApplicationStatus.Open);

            var createResult = await _httpHelpers.CreateApplication(testData);

            createResult.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public async Task BuildersRisk_Get_ShouldReturnCorrectSchema()
        {
            var testData = _buildersRiskHelpers.GetDefaultTestData(BuildersRiskApplicationStatus.Open);

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
        public async Task BuildersRisk_Submit_ShouldReturnOkStatus()
        {
            var testData = _buildersRiskHelpers.GetDefaultTestData(BuildersRiskApplicationStatus.Submitted);

            var result = await _httpHelpers.CreateApplication(testData);

            await _httpHelpers.CreateQuote(result.ApplicationId);

            var submitStatus = await _httpHelpers.SubmitApplication(result.ApplicationId);

            submitStatus.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task BuildersRisk_AddAttachment_ShouldReturnOkStatus()
        {
            var testData = _buildersRiskHelpers.GetDefaultTestData(BuildersRiskApplicationStatus.Submitted);

            var result = await _httpHelpers.CreateApplication(testData);

            await _httpHelpers.CreateQuote(result.ApplicationId);

            var attachment = _buildersRiskHelpers.CreateAttachment("ConstructionBudget");

            var attachmentResult = await _httpHelpers.UploadAttachment(result.ApplicationId, attachment);
            
            attachmentResult.StatusCode.Should().Be(HttpStatusCode.Created);
            attachmentResult.AttachmentId.Should().NotBe(-1);
        }

        [Test]
        public async Task BuildersRisk_Approve_ShouldReturnOkStatus()
        {
            var testData = _buildersRiskHelpers.GetDefaultTestData(BuildersRiskApplicationStatus.Submitted);

            var result = await _httpHelpers.CreateApplication(testData);

            await _httpHelpers.CreateQuote(result.ApplicationId);
            await _httpHelpers.SubmitApplication(result.ApplicationId);

            var approveResult = await _httpHelpers.CreateApprovedQuote(result.ApplicationId, true);

            approveResult.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task BuildersRisk_Review_ShouldReturnOkStatus()
        {
            var testData = _buildersRiskHelpers.GetDefaultTestData(BuildersRiskApplicationStatus.Submitted);

            var result = await _httpHelpers.CreateApplication(testData);

            await _httpHelpers.CreateQuote(result.ApplicationId);
            await _httpHelpers.SubmitApplication(result.ApplicationId);

            var update = new QuoteUpdate { Subtotal = 999999 };

            var reviewResponse = await _httpHelpers.ReviewQuote(result.ApplicationId, update);

            JObject appObj = JObject.Parse(reviewResponse.Quote);

            var newSubtotal = decimal.Parse(appObj.GetValue("subtotal").ToString());

            newSubtotal.Should().Be(update.Subtotal);

            reviewResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task BuildersRisk_Revise_ShouldReturnOkStatus()
        {
            var testData = _buildersRiskHelpers.GetDefaultTestData(BuildersRiskApplicationStatus.Submitted);

            var result = await _httpHelpers.CreateApplication(testData);

            await _httpHelpers.CreateQuote(result.ApplicationId);
            await _httpHelpers.SubmitApplication(result.ApplicationId);

            var update = new QuoteUpdate { Subtotal = 999999 };

            await _httpHelpers.ReviewQuote(result.ApplicationId, update);

            var reviseStatus = await _httpHelpers.ReviseQuote(result.ApplicationId, "QA Revise Quote Test");

            reviseStatus.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task BuildersRisk_RequestBinder_ShouldReturnOkStatus()
        {
            var testData = _buildersRiskHelpers.GetDefaultTestData(BuildersRiskApplicationStatus.Submitted);

            var appResult = await _httpHelpers.CreateApplication(testData);
            var applicationId = appResult.ApplicationId;

            await _httpHelpers.CreateQuote(applicationId);
            await _httpHelpers.SubmitApplication(applicationId);
            await _httpHelpers.CreateApprovedQuote(applicationId, true);

            var requestBinderStatus = await _httpHelpers.RequestBinder(applicationId);

            requestBinderStatus.Should().Be(HttpStatusCode.OK);
        }
    }
}
