using Chubb.Bop.Application;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using NUnit.Framework;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Features.ChubbBop
{
    [TestFixture]
    public class ApplicationServiceTestFixture
    {
        private HttpTestHelpers _httpHelpers;
        private ChubbBopTestHelpers _chubbHelpers;
        private JsonSchema4 _schema;
        private readonly string _productId = ConfigurationManager.AppSettings["ChubbProductId"];

        [SetUp]
        public async Task Setup()
        {
            _httpHelpers = new HttpTestHelpers();
            _chubbHelpers = new ChubbBopTestHelpers();
            _schema = await _httpHelpers.GetJsonSchemaFromType<ChboApplicationResource>();
        }

        [TearDown]
        public async Task Cleanup()
        {
            await _httpHelpers.Cleanup();
        }

        [Test]
        public async Task Chubb_Create_ShouldReturnCreatedStatus()
        {
            var testData = _chubbHelpers.GetDefaultTestData(ChubbApplicationStatus.Open);

            var createResult = await _httpHelpers.CreateApplication(testData);

            createResult.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public async Task Chubb_Get_ShouldReturnCorrectSchema()
        {
            var testData = _chubbHelpers.GetDefaultTestData(ChubbApplicationStatus.Open);

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
        public async Task Chubb_Bind_ShouldReturnOkStatus()
        {
            var testData = _chubbHelpers.GetDefaultTestData(ChubbApplicationStatus.Bound);

            var result = await _httpHelpers.CreateApplication(testData);

            var bindResult = await _httpHelpers.BindQuote(result.ApplicationId);

            bindResult.StatusCode.Should().Be(HttpStatusCode.OK);

            bindResult.PolicyNumber.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Chubb_RequestEndorsement_ShouldReturnOk()
        {
            var testData = _chubbHelpers.GetDefaultTestData(ChubbApplicationStatus.Bound);

            var result = await _httpHelpers.CreateApplication(testData);

            await _httpHelpers.BindQuote(result.ApplicationId);

            var endorsementRequest = new Models.EndorsementRequest { RequestText = "requesting endorsement test" };

            var endorsementResult = await _httpHelpers.RequestEndorsement(_productId, result.ApplicationId, endorsementRequest);

            endorsementResult.Should().Be(HttpStatusCode.OK);
        }
    }
}
