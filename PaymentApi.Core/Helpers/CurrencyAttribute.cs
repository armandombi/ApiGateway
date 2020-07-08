﻿using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace PaymentApi.Core.Helpers
{
    public class CurrencyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var symbol = CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .Where(c => !c.IsNeutralCulture)
                .Select(culture =>
                {
                    try
                    {
                        return new RegionInfo(culture.Name);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(ri => ri != null && ri.ISOCurrencySymbol == (string)value)
                .Select(ri => ri.CurrencySymbol)
                .FirstOrDefault();

            return symbol != null;

        }
    }
}