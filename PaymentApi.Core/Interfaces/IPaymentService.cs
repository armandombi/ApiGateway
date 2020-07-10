using PaymentApi.Core.Models;
using System;
using System.Threading.Tasks;

namespace PaymentApi.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(Guid id, PaymentRequest request);
        Task<PaymentDocument> GetPayment(Guid id);
    }
}
