using PaymentApi.Core.Interfaces;
using PaymentApi.Core.Models;
using PaymentApi.Core.Models.Enums;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PaymentApi.Core.Helpers;
using PaymentApi.Core.Settings;
using Serilog;

namespace PaymentApi.Infrastructure.Services
{
    public class AcquiringBankService : IAcquiringBankService
    {
        private readonly HttpClient _httpClient;
        public AcquiringBankService(IOptions<BankApiSettings> settings)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(settings.Value.BaseUrl ?? ""),
                Timeout = TimeSpan.FromSeconds(settings.Value.TimeoutSeconds)
            };
        }
        public async Task<BankResponse> SendPayment(PaymentRequest request)
        {
            try
            {
                var content = JsonHelper.ConvertToByteContent(request);
                var bankResponse = await _httpClient.PostAsync("/payment", content);

                return bankResponse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<BankResponse>(await bankResponse.Content.ReadAsStringAsync()) : new BankResponse{Id = Guid.Empty, Status = TransactionStatus.Failed};
            }
            catch (Exception exception)
            {
                Log.Error($"{MethodBase.GetCurrentMethod()?.DeclaringType} failed with exception {JsonConvert.SerializeObject(exception)}");
                throw;
            }
        }
    }
}
