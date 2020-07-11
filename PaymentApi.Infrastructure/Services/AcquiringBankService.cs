using Newtonsoft.Json;
using PaymentApi.Core.Helpers;
using PaymentApi.Core.Interfaces;
using PaymentApi.Core.Models;
using PaymentApi.Core.Models.Enums;
using Serilog;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace PaymentApi.Infrastructure.Services
{
    public class AcquiringBankService : IAcquiringBankService
    {
        private readonly HttpClient _httpClient;
        public AcquiringBankService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<BankResponse> SendPayment(PaymentRequest request)
        {
            try
            {
                var content = JsonHelper.ConvertToByteContent(request);
                var bankResponse = await _httpClient.PostAsync("/payment", content);

                return bankResponse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<BankResponse>(await bankResponse.Content.ReadAsStringAsync()) : new BankResponse { Id = Guid.Empty, Status = TransactionStatus.Failed };
            }
            catch (Exception exception)
            {
                Log.Error($"{MethodBase.GetCurrentMethod()?.DeclaringType} failed with exception {JsonConvert.SerializeObject(exception)}");
                throw;
            }
        }
    }
}
