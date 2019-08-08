using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IntegrationTests.Rps.Products.Models;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using NJsonSchema.Generation;
using static Rps.Products.Api.Controllers.PaymentController;

namespace IntegrationTests.Rps.Products.Features
{
    public class HttpTestHelpers
    {
        private readonly string carrierApiToken = ConfigurationManager.AppSettings["BuildersRiskCarrierApiToken"];
        private readonly string retailerApiToken = ConfigurationManager.AppSettings["RetailerApiToken"];
        private readonly string serviceBaseUri = ConfigurationManager.AppSettings["ServiceBaseUri"];
        private readonly string underwriterApiToken = ConfigurationManager.AppSettings["underwriterApiToken"];
        private List<int> _idsToCleanup = new List<int>();

        public async Task<BindResult> BindQuote(int applicationId)
        {
            await CreateQuote(applicationId);
            var requestUri = $"application/{applicationId}/Bind";

            var response = await GetStatusAndContent<string>(CreateRequestMessage(HttpMethod.Post, requestUri, retailerApiToken));

            return new BindResult { StatusCode = response.StatusCode, PolicyNumber = response.Content };
        }

        public async Task Cleanup()
        {
            foreach (var applicationId in _idsToCleanup)
            {
                await DeleteApplication(applicationId);
            }
        }

        public async Task<CreateApplicationResult> CreateApplication<T>(T application)
        {
            var requestUri = "application";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Post, requestUri, retailerApiToken, application));

            int applicationId = -1;
            if (response.StatusCode == HttpStatusCode.Created)
            {
                JObject applicationObj = JObject.Parse(response.Content);

                applicationId = int.Parse(applicationObj.GetValue("id").ToString());

                _idsToCleanup.Add(applicationId);
            }

            return new CreateApplicationResult { StatusCode = response.StatusCode, ApplicationId = applicationId };
        }

        public async Task<HttpStatusCode> CreateApprovedQuote(int applicationId, bool isCarrier = false)
        {
            var requestUri = $"application/{applicationId}/approve";
            var token = isCarrier ? carrierApiToken : underwriterApiToken;

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Post, requestUri, token));

            return response.StatusCode;
        }

        public async Task<QuoteResult> CreateQuote(int applicationId, bool isUnderwriter = false)
        {
            var requestUri = $"application/{applicationId}/quote";
            var apiToken = isUnderwriter ? underwriterApiToken : retailerApiToken;

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Post, requestUri, apiToken));

            return new QuoteResult { StatusCode = response.StatusCode, Quote = response.Content };
        }

        public async Task<HttpStatusCode> DeclineQuote(int applicationId, string reason)
        {
            await SubmitApplication(applicationId);
            var requestUri = $"application/{applicationId}/decline";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Post, requestUri, underwriterApiToken, reason));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteApplication(int applicationId)
        {
            var requestUri = $"application/{applicationId}/delete";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Delete, requestUri, retailerApiToken));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> ExtendQuote(int applicationId)
        {
            await SubmitApplication(applicationId);
            await CreateQuote(applicationId, true);
            await CreateApprovedQuote(applicationId);

            var requestUri = $"application/{applicationId}/extend";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Post, requestUri, underwriterApiToken));

            return response.StatusCode;
        }

        public async Task<ApplicationResult> GetApplication(int applicationId)
        {
            var requestUri = $"application/{applicationId}";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Get, requestUri, retailerApiToken));

            return new ApplicationResult { StatusCode = response.StatusCode, Application = response.Content };
        }

        public Task<JsonSchema4> GetJsonSchemaFromType<T>()
        {
            var settings = new JsonSchemaGeneratorSettings { DefaultPropertyNameHandling = PropertyNameHandling.CamelCase, FlattenInheritanceHierarchy = true };
            return JsonSchema4.FromTypeAsync<T>(settings);
        }

        public async Task<QuestionResource[]> GetQuestionsForClassification(string productId, string classType, int classId)
        {
            var requestUri = $"{productId}/application/{classType}/{classId}/questions";

            var response = await GetStatusAndContent<QuestionResource[]>(CreateRequestMessage(HttpMethod.Get, requestUri, retailerApiToken));

            return response.Content;
        }

        public async Task<QuoteResult> GetQuote(int applicationId)
        {
            var requestUri = $"application/{applicationId}/quote/review";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Get, requestUri, retailerApiToken));

            return new QuoteResult { StatusCode = response.StatusCode, Quote = response.Content };
        }

        public async Task<HttpStatusCode> QaBackdate(string productId, int applicationId)
        {
            var requestUri = $"{productId}/application/{applicationId}/QaBackdate";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Put, requestUri, underwriterApiToken, "true"));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> RequestBinder(int applicationId)
        {
            var requestUri = $"application/{applicationId}/RequestBinder";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Post, requestUri, retailerApiToken));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> RequestEndorsement(string productId, int applicationId, EndorsementRequest endorsementRequest)
        {
            var requestUri = $"{productId}/application/{applicationId}/endorsementRequest";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Put, requestUri, retailerApiToken, endorsementRequest));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> RequestPayment(int applicationId, PaymentStatusRequest paymentStatusRequest)
        {
            var requestUri = $"application/{applicationId}/payment";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Post, requestUri, retailerApiToken, paymentStatusRequest));

            return response.StatusCode;
        }

        public async Task<QuoteResult> ReviewQuote<T>(int applicationId, T update)
        {
            var requestUri = $"application/{applicationId}/quote/review";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Put, requestUri, carrierApiToken, update));

            return new QuoteResult { StatusCode = response.StatusCode, Quote = response.Content };
        }

        public async Task<HttpStatusCode> ReviseQuote(int applicationId, string comments)
        {
            var requestUri = $"application/{applicationId}/revise";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Post, requestUri, carrierApiToken, comments));

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> SubmitApplication(int applicationId)
        {
            var requestUri = $"application/{applicationId}/submit";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Post, requestUri, retailerApiToken));

            return response.StatusCode;
        }

        public async Task<QuoteResult> UpdateQuote<T>(int applicationId, T update)
        {
            await SubmitApplication(applicationId);
            await CreateQuote(applicationId, true);
            var requestUri = $"application/{applicationId}/quote/review";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Put, requestUri, underwriterApiToken, update));

            return new QuoteResult { StatusCode = response.StatusCode, Quote = response.Content };
        }

        public async Task<AttachmentResult> UploadAttachment(int applicationId, AttachmentResource attachment)
        {
            var requestUri = $"application/{applicationId}/attachment";

            var response = await GetStatusAndContent(CreateRequestMessage(HttpMethod.Put, requestUri, retailerApiToken, attachment));

            int attachmentId = -1;
            if (response.StatusCode == HttpStatusCode.Created)
            {
                JObject attachmentResultObj = JObject.Parse(response.Content);

                attachmentId = int.Parse(attachmentResultObj.GetValue("attachmentId").ToString());
            }

            return new AttachmentResult { StatusCode = response.StatusCode, AttachmentId = attachmentId };
        }

        private HttpRequestMessage CreateRequestMessage(HttpMethod method, string requestUri, string token)
        {
            return this.CreateRequestMessage<object>(method, requestUri, token, null);
        }

        private HttpRequestMessage CreateRequestMessage<T>(HttpMethod method, string requestUri, string token, T requestObject)
        {
            var request = new HttpRequestMessage(method, $"{serviceBaseUri}{requestUri}");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (requestObject != null)
            {
                request.Content = new ObjectContent<T>(requestObject, new JsonMediaTypeFormatter());
            }

            return request;
        }

        private async Task<HttpClientResult<T>> GetStatusAndContent<T>(HttpRequestMessage requestMessage)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.SendAsync(requestMessage))
                {
                    var result = await response.Content.ReadAsAsync<T>();

                    return new HttpClientResult<T>(response.StatusCode, result);
                }
            }
        }

        private async Task<HttpClientResult<string>> GetStatusAndContent(HttpRequestMessage requestMessage)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.SendAsync(requestMessage))
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return new HttpClientResult<string>(response.StatusCode, result);
                }
            }
        }

        private class HttpClientResult<T>
        {
            public HttpClientResult(HttpStatusCode statusCode, T content)
            {
                this.StatusCode = statusCode;
                this.Content = content;
            }

            public T Content { get; private set; }
            public HttpStatusCode StatusCode { get; private set; }
        }
    }
}