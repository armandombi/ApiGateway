using PaymentApi.Core.Models;

namespace ApiGateway.Tests
{
    internal static class PaymentRequestGenerator
    {
        internal static PaymentRequest GenerateValidPaymentRequest()
        {
            return new PaymentRequest
            {
                Amount = 1,
                Currency = "EUR",
                CardNumber = "30391241159448",
                CardHolderName = "John Doe",
                ExpiryMonth = 12,
                ExpiryYear = 2021,
                SecurityCode = "123"
            };
        }

        internal static PaymentRequest GenerateInvalidPaymentRequest()
        {
            return new PaymentRequest
            {
                Amount = 1,
                Currency = "EURO",
                CardNumber = "4539625828647723",
                CardHolderName = "John Doe",
                ExpiryMonth = 12,
                ExpiryYear = 2019,
                SecurityCode = "12345"
            };
        }

        internal static PaymentRequest GenerateInvalidBankPaymentRequest()
        {
            return new PaymentRequest
            {
                Amount = 10,
                Currency = "USD",
                CardNumber = "3539705559222722",
                CardHolderName = "FailedRequest",
                ExpiryMonth = 12,
                ExpiryYear = 2021,
                SecurityCode = "123"
            };
        }
    }
}