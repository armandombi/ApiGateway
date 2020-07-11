using System;
using System.Collections.Generic;
using System.Text;
using PaymentApi.Core.Helpers;
using PaymentApi.Core.Models.Enums;

namespace PaymentApi.Core.Models.DTO
{
    public class PaymentDto
    {
        public PaymentDto()
        {
            
        }
        public PaymentDto(PaymentDocument payment)
        {
            Id = payment.Id;
            Status = payment.Status;
            Created = payment.Created;
            Amount = payment.Amount;
            Currency = payment.Currency;
            CardNumber = payment.CardNumber.MaskCardNumber();
            CardHolderName = payment.CardHolderName;
            BankPaymentId = payment.BankPaymentId;
        }

        public Guid Id { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTimeOffset Created { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public Guid BankPaymentId { get; set; }
    }
}
