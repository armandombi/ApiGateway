using PaymentApi.Core.Interfaces;
using PaymentApi.Core.Models;
using PaymentApi.Core.Models.Enums;
using System;
using System.Threading.Tasks;

namespace PaymentApi.Infrastructure.Services
{
    public class AcquiringBankService : IAcquiringBankService
    {
        public async Task<BankResponse> SendPayment(PaymentRequest request)
        {
            var random = new Random();
            var bankResponse = random.Next(0, 1);
            return bankResponse == 0 ? new BankResponse() { Id = Guid.NewGuid(), Status = TransactionStatus.Complete } : new BankResponse() { Id = Guid.NewGuid(), Status = TransactionStatus.Failed };
        }
    }
}
