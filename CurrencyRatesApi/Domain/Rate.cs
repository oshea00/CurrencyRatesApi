using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyRatesApi.Domain
{
    public class Rate
    {
        public string CurrencyCode { get; set; }
        public decimal ConversionRate { get; set; }
    }
}