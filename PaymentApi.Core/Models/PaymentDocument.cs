﻿using System;
using System.Collections.Generic;
using System.Text;
using PaymentApi.Core.Models.Enums;

namespace PaymentApi.Core.Models
{
    public class PaymentDocument
    {
        public Guid Id { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTimeOffset Created { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardNumber { get; set; }
        public Guid BankPaymentId { get; set; }
    }
}