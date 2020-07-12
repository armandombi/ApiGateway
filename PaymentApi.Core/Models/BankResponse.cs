using System;
using PaymentApi.Core.Models.Enums;

namespace PaymentApi.Core.Models
{
    public class BankResponse
    {
        public Guid Id { get; set; }
        public TransactionStatus Status { get; set; }
    }
}