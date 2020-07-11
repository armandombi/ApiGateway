using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentApi.Core.Helpers
{
    /// <summary>
    /// Extension method to deal with operations on the Payment Card
    /// </summary>
    public static class CardHelper
    {
        /// <summary>
        /// Adds a special character to mask the card number and only reveal the last 4 digits
        /// </summary>
        /// <param name="cardNumber">The payment card number</param>
        /// <returns>The masked payment card number</returns>
        public static string MaskCardNumber(this string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 13)
                return string.Empty;

            var lastDigits = cardNumber.Substring(cardNumber.Length - 4, 4);
            return $"{new string('*', cardNumber.Length - lastDigits.Length)}{lastDigits}";
        }
    }
}
