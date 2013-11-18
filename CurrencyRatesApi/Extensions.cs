using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyRatesApi
{
    public static class Extensions
    {
        public static DateTime DateTimeFromUnix(this double timestamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(timestamp).ToLocalTime();
            return dt;
        }
    }
}