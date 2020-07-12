using System;
using System.Threading.Tasks;
using PaymentApi.Core.Models;

namespace PaymentApi.Core.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(Guid id, PaymentRequest request);
        Task<PaymentDocument> GetPayment(Guid id);
    }
}