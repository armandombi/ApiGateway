using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentApi.Core.Settings
{
    public class BankApiSettings
    {
        public string BaseUrl { get; set; }
        public int TimeoutSeconds { get; set; }
    }
}
