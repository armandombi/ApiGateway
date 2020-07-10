using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentApi.Core.Helpers
{
    public static class CardHelper
    {
        public static string MaskCardNumber(this string cardNumber)
        {
            var lastDigits = cardNumber.Substring(cardNumber.Length - 4, 4);
            return $"{new string('*', cardNumber.Length - lastDigits.Length)}{lastDigits}";
        }
    }
}
