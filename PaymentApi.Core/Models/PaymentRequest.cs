using System.ComponentModel.DataAnnotations;
using PaymentApi.Core.Helpers;

namespace PaymentApi.Core.Models
{
    public class PaymentRequest
    {
        [Required] [Range(0, 600000)] 
        public decimal Amount { get; set; }

        [Required] [Currency] 
        public string Currency { get; set; }

        [Required] [CreditCard] 
        public string CardNumber { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        public string CardHolderName { get; set; }

        [Required] [Range(1, 12)] 
        public int ExpiryMonth { get; set; }

        [Required] [Range(2020, 2030)] 
        public int ExpiryYear { get; set; }

        [Required]
        [SecurityCode]
        [MinLength(3)]
        [MaxLength(4)]
        public string SecurityCode { get; set; }
    }
}