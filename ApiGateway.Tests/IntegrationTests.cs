using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PaymentApi.Core.Helpers;
using PaymentApi.Core.Models.DTO;
using PaymentApi.Core.Models.Enums;
using Xunit;

namespace ApiGateway.Tests
{
    public class IntegrationTests
    {
        public IntegrationTests()
        {
            var paymentApi = new ApiApplicationFactory();

            Client = paymentApi.CreateClient();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "IntegrationTestUser");
        }

        private HttpClient Client { get; }

        [Fact]
        public async Task PostInvalidAuthorizationPaymentApi_GetInvalidAuthorizationResultPaymentApi()
        {
            var request = PaymentRequestGenerator.GenerateInvalidPaymentRequest();
            var guid = Guid.NewGuid();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Lorem", "Ipsum");

            var content = JsonHelper.ConvertToByteContent(request);
            var postResponse = await Client.PostAsync($"v1/payment/{guid}", content);

            Assert.False(postResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Unauthorized, postResponse.StatusCode);
        }

        [Fact]
        public async Task PostInvalidBankPaymentRequest_GetInvalidResultPaymentApi()
        {
            var request = PaymentRequestGenerator.GenerateInvalidBankPaymentRequest();
            var guid = Guid.NewGuid();

            var content = JsonHelper.ConvertToByteContent(request);
            var postResponse = await Client.PostAsync($"v1/payment/{guid}", content);

            Assert.False(postResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        }

        [Fact]
        public async Task PostInvalidRequestPaymentApi_GetInvalidResultPaymentApi()
        {
            var request = PaymentRequestGenerator.GenerateInvalidPaymentRequest();
            var guid = Guid.NewGuid();

            var content = JsonHelper.ConvertToByteContent(request);
            var postResponse = await Client.PostAsync($"v1/payment/{guid}", content);

            Assert.False(postResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        }

        [Fact]
        public async Task PostValidRequestPaymentApi_GetValidResponsePaymentApi()
        {
            var request = PaymentRequestGenerator.GenerateValidPaymentRequest();
            var guid = Guid.NewGuid();
            var maskedCardNumber = request.CardNumber.MaskCardNumber();

            var content = JsonHelper.ConvertToByteContent(request);
            var postResponse = await Client.PostAsync($"v1/payment/{guid}", content);

            Assert.True(postResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
            Assert.Equal($"/v1/payment/{guid}", postResponse.Headers.Location.OriginalString);

            var getResponse = await Client.GetAsync($"v1/payment/{guid}");

            Assert.True(getResponse.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var paymentDetails =
                JsonConvert.DeserializeObject<PaymentDto>(await getResponse.Content.ReadAsStringAsync());

            Assert.Equal(guid, paymentDetails.Id);
            Assert.Equal(maskedCardNumber, paymentDetails.CardNumber);
            Assert.Equal(PaymentStatus.Complete, paymentDetails.Status);
        }
    }
}