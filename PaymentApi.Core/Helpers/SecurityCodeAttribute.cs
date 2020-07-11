using Newtonsoft.Json;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentApi.Core.Helpers
{
    internal class SecurityCodeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates if the payment request security code is valid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            try
            {
                return int.TryParse((string)value, out _);
            }
            catch (Exception ex)
            {
                Log.Error($"Security code validation failed for value {JsonConvert.SerializeObject(value)} with exception {JsonConvert.SerializeObject(ex)}");
                return false;
            }
        }
    }
}
