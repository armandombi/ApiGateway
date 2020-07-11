using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PaymentApi.Core.Models;

namespace PaymentApi.Core.Interfaces
{
    public interface IAcquiringBankService
    {
        Task<BankResponse> SendPayment(PaymentRequest request);
    }
}
