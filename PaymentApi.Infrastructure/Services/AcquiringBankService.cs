using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PaymentApi.Core.Interfaces;
using PaymentApi.Core.Models;

namespace PaymentApi.Infrastructure.Services
{
    public class AcquiringBankService : IAcquiringBankService
    {
        public Task<BankResponse> SendPayment(PaymentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
