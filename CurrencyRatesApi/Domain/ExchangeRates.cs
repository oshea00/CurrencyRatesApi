using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurrencyRatesApi.Domain
{
    public class ExchangeRates
    {
        public ExchangeRates()
        {
            Rates = new Dictionary<string, decimal>();
        }
        public DateTime EffectiveDate { get; set; }
        public String BaseCurrencyCode { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
