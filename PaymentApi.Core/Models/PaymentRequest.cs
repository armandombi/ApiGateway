using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentApi.Core.Models
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string SecurityCode { get; set; }
    }
}
