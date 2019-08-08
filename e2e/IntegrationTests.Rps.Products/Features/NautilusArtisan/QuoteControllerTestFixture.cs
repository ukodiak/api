using FluentAssertions;
using NUnit.Framework;
using Rps.Products.Rating;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using QuoteUpdate = IntegrationTests.Rps.Products.Models.NautilusArtisan.QuoteUpdate;
using ArticleUpdate = IntegrationTests.Rps.Products.Models.NautilusArtisan.ArticleUpdate;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using Rps.Products.Rating.Forms;

namespace IntegrationTests.Rps.Products.Features.NautilusArtisan
{
    [TestFixture]
    public class QuoteControllerTestFixture
    {
        private HttpTestHelpers _testHelpers;
        private NautilusTestHelpers _nautilusHelpers;
        private JsonSchema4 _quoteSchema;

        [SetUp]
        public async Task Setup()
        {
            _testHelpers = new HttpTestHelpers();
            _nautilusHelpers = new NautilusTestHelpers();

            _quoteSchema = await _testHelpers.GetJsonSchemaFromType<QuoteResource>();
        }

        [Test]
        public async Task Artisans_GetQuote_ShouldReturnCorrectSchema()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Quoted);

            var applicationResult = await _testHelpers.CreateApplication(testData);

            var applicationId = applicationResult.ApplicationId;

            await _testHelpers.CreateQuote(applicationId);

            var quoteResult = await _testHelpers.GetQuote(applicationId);

            quoteResult.StatusCode.Should().Be(HttpStatusCode.OK);

            JObject quoteObj = JObject.Parse(quoteResult.Quote);

            var schemaData = _quoteSchema.ToJson();
            var schemaErrors = _quoteSchema.Validate(quoteObj);

            schemaErrors.Count.Should().Be(0);                        
        }

        [Test]
        public async Task Artisans_Quote_ShouldCreateQuoteRecord()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Quoted);

            var applicationResult = await _testHelpers.CreateApplication(testData);

            var applicationId = applicationResult.ApplicationId;

            var createQuoteResponse = await _testHelpers.CreateQuote(applicationId);

            createQuoteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            JObject quoteObj = JObject.Parse(createQuoteResponse.Quote);

            var quote = quoteObj.ToObject<QuoteResource>();

            quote.Total.Should().BeGreaterThan(0);            
        }
        
        [Test]
        public async Task Artisans_UpdateQuote_ShouldReturnOkStatus()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Quoted);

            var applicationResult = await _testHelpers.CreateApplication(testData);

            var applicationId = applicationResult.ApplicationId;

            var quoteUpdate = new QuoteUpdate()
            {                
                Terms = new List<ArticleUpdate>()
                {
                    new ArticleUpdate()
                    {
                        Key = "GLADJ",                        
                        Premium = 999                        
                    }
                }
            };

            var response = await _testHelpers.UpdateQuote(applicationId, quoteUpdate);
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);            
        }
        
        [Test]
        public async Task Artisans_QuoteForms_ShouldIncludeL292WhenNewVenture()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Quoted);

            testData.Business.IsNewVenture = true;

            var applicationResult = await _testHelpers.CreateApplication(testData);

            var applicationId = applicationResult.ApplicationId;

            var createQuoteResponse = await _testHelpers.CreateQuote(applicationId);

            JObject quoteObj = JObject.Parse(createQuoteResponse.Quote);

            var quote = quoteObj.ToObject<QuoteResource>();

            var forms = quote.Forms;

            var foundForm = IsFormFound("L292", forms);

            foundForm.Should().Be(true);
        }

        [Test]
        public async Task Artisans_QuoteForms_ShouldIncludeL292WhenLapseInCoverage()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Quoted);

            testData.Business.HasCoverageLapse = true;

            var applicationResult = await _testHelpers.CreateApplication(testData);

            var applicationId = applicationResult.ApplicationId;

            var createQuoteResponse = await _testHelpers.CreateQuote(applicationId);
            
            JObject quoteObj = JObject.Parse(createQuoteResponse.Quote);

            var quote = quoteObj.ToObject<QuoteResource>();

            var forms = quote.Forms;

            var foundForm = IsFormFound("L292", forms);

            foundForm.Should().Be(true);
        }

        [Test]
        public async Task Artisans_QuoteForms_ShouldNotIncludeL292WhenNoLapseInCoverage()
        {
            var testData = _nautilusHelpers.GetDefaultTestData(ArtisansApplicationStatus.Quoted);

            testData.Business.HasCoverageLapse = false;

            var applicationResult = await _testHelpers.CreateApplication(testData);

            var applicationId = applicationResult.ApplicationId;

            var createQuoteResponse = await _testHelpers.CreateQuote(applicationId);

            JObject quoteObj = JObject.Parse(createQuoteResponse.Quote);

            var quote = quoteObj.ToObject<QuoteResource>();

            var forms = quote.Forms;

            var foundForm = IsFormFound("L292", forms);

            foundForm.Should().Be(false);
        }

        private bool IsFormFound(string formName, FormResource[] forms)
        {
            var foundForm = false;
            foreach (var form in forms)
            {
                if (form.Name == formName)
                {
                    foundForm = true;
                }
            }

            return foundForm;
        }
    }
}
