using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PaymentApi.Core.Interfaces;
using PaymentApi.Core.Models;

namespace PaymentApi.Core.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<bool> ProcessPayment(Guid id, PaymentRequest request)
        {
            return true;
        }
    }
}
