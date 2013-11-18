using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CurrencyRatesApi;
using CurrencyRatesApi.Repositories;
using System.Configuration;
using System.Net;
using System.IO;
using CurrencyRatesApi.Domain;
using ServiceStack.Text;

namespace CurrencyRatesApi.Tests
{
    [TestFixture]
    public class ExchangeRatesRepoTests
    {
        [Test]
        public void CanGetUriPathFromConfig()
        {
            var uriPath = ConfigurationManager.AppSettings.Get("OpenExchangeRatesUri");
            Assert.IsFalse(String.IsNullOrEmpty(uriPath));
        }

        [Test]
        public void CanGetAppIdFromConfig()
        {
            var appId = ConfigurationManager.AppSettings.Get("OpenExchangeRatesAppId");
            Assert.IsFalse(String.IsNullOrEmpty(appId));
        }

        [Test]
        public void CanGetCurrentRatesFromRepo()
        {
            var repo = new ExchangeRatesRepository();
            var rates = repo.GetCurrentRates();
            Assert.AreEqual("USD",rates.BaseCurrencyCode);
        }

        [Test]
        public void CanGetHistoricalRatesFromRepo()
        {
            var repo = new ExchangeRatesRepository();
            var rates = repo.GetHistoricalRates(new DateTime(2013,11,1));
            Assert.AreEqual("USD", rates.BaseCurrencyCode);
        }

        [Test]
        public void Can_Deserialize_CurrenctExchangeRates_Using_ServiceStack_Json()
        {
            var json = TestRatesJson();

            var currRates = JsonObject.Parse(json)
                .ConvertTo<ExchangeRates>(x =>
                    new ExchangeRates
                    {
                        EffectiveDate = x.JsonTo<double>("timestamp").DateTimeFromUnix(),
                        BaseCurrencyCode = x.Get("base"),
                        Rates = x.JsonTo<Dictionary<string,decimal>>("rates"),
                    }
                );

            Assert.AreEqual("USD", currRates.BaseCurrencyCode);
            Assert.Greater(currRates.Rates.Keys.Count, 0);
        }

        public string TestRatesJson()
        {
            return @"
            {
              ""disclaimer"": ""Exchange rates are provided for informational purposes only, and do not constitute financial advice of any kind. Although every attempt is made to ensure quality, NO guarantees are given whatsoever of accuracy, validity, availability, or fitness for any purpose - please use at your own risk. All usage is subject to your acceptance of the Terms and Conditions of Service, available at: https://openexchangerates.org/terms/"",
              ""license"": ""Data sourced from various providers with public-facing APIs; copyright may apply; resale is prohibited; no warranties given of any kind. All usage is subject to your acceptance of the License Agreement available at: https://openexchangerates.org/license/"",
              ""timestamp"": 1384729260,
              ""base"": ""USD"",
              ""rates"": {
                ""AED"": 3.6731,
                ""AFN"": 57.1957,
                ""ALL"": 104.011,
                ""AMD"": 408.320997,
                ""ANG"": 1.789,
                ""AOA"": 97.27345,
                ""ARS"": 5.990146,
                ""AUD"": 1.06655,
                ""AWG"": 1.78985,
                ""AZN"": 0.7842,
                ""BAM"": 1.449976,
                ""BBD"": 2,
                ""BDT"": 77.483919,
                ""BGN"": 1.45239,
                ""BHD"": 0.37679,
                ""BIF"": 1547.1901,
                ""BMD"": 1,
                ""BND"": 1.24458,
                ""BOB"": 6.89554,
                ""BRL"": 2.31777,
                ""BSD"": 1,
                ""BTC"": 0.00206497,
                ""BTN"": 63.150437,
                ""BWP"": 8.629992,
                ""BYR"": 9295.75,
                ""BZD"": 1.98992,
                ""CAD"": 1.043845,
                ""CDF"": 923.6445,
                ""CHF"": 0.914692,
                ""CLF"": 0.02242,
                ""CLP"": 520.0413,
                ""CNY"": 6.107682,
                ""COP"": 1918.851667,
                ""CRC"": 500.958002,
                ""CUP"": 22.687419,
                ""CVE"": 81.799341,
                ""CZK"": 20.11623,
                ""DJF"": 177.231699,
                ""DKK"": 5.526957,
                ""DOP"": 42.30834,
                ""DZD"": 80.41152,
                ""EEK"": 11.6983,
                ""EGP"": 6.87886,
                ""ERN"": 15.002825,
                ""ETB"": 18.98354,
                ""EUR"": 0.74103,
                ""FJD"": 1.850039,
                ""FKP"": 0.620267,
                ""GBP"": 0.620267,
                ""GEL"": 1.6761,
                ""GHS"": 2.2511,
                ""GIP"": 0.620267,
                ""GMD"": 32.614261,
                ""GNF"": 6986.066667,
                ""GTQ"": 7.89695,
                ""GYD"": 201.901666,
                ""HKD"": 7.75384,
                ""HNL"": 20.4793,
                ""HRK"": 5.65519,
                ""HTG"": 42.23382,
                ""HUF"": 220.993801,
                ""IDR"": 11618.083333,
                ""ILS"": 3.524282,
                ""INR"": 62.79068,
                ""IQD"": 1161.983367,
                ""IRR"": 24849.25,
                ""ISK"": 121.662,
                ""JEP"": 0.620267,
                ""JMD"": 103.476999,
                ""JOD"": 0.704864,
                ""JPY"": 100.224401,
                ""KES"": 86.347161,
                ""KGS"": 48.733575,
                ""KHR"": 3998.744667,
                ""KMF"": 364.966559,
                ""KPW"": 900,
                ""KRW"": 1063.083325,
                ""KWD"": 0.283101,
                ""KYD"": 0.825206,
                ""KZT"": 153.068,
                ""LAK"": 7934.65,
                ""LBP"": 1505.916667,
                ""LKR"": 131.151999,
                ""LRD"": 80.566033,
                ""LSL"": 10.177538,
                ""LTL"": 2.559445,
                ""LVL"": 0.520948,
                ""LYD"": 1.25227,
                ""MAD"": 8.30425,
                ""MDL"": 12.91624,
                ""MGA"": 2262.2,
                ""MKD"": 45.68592,
                ""MMK"": 970.684,
                ""MNT"": 1716.166667,
                ""MOP"": 7.9691,
                ""MRO"": 299.97,
                ""MTL"": 0.683738,
                ""MUR"": 30.60154,
                ""MVR"": 15.38396,
                ""MWK"": 400,
                ""MXN"": 12.93462,
                ""MYR"": 3.19975,
                ""MZN"": 29.9756,
                ""NAD"": 10.16525,
                ""NGN"": 158.577999,
                ""NIO"": 24.99772,
                ""NOK"": 6.107788,
                ""NPR"": 100.2627,
                ""NZD"": 1.196991,
                ""OMR"": 0.385035,
                ""PAB"": 1,
                ""PEN"": 2.7937,
                ""PGK"": 2.55695,
                ""PHP"": 43.59584,
                ""PKR"": 107.224,
                ""PLN"": 3.10224,
                ""PYG"": 4426.056706,
                ""QAR"": 3.64085,
                ""RON"": 3.301032,
                ""RSD"": 84.508641,
                ""RUB"": 32.5956,
                ""RWF"": 666.7959,
                ""SAR"": 3.75051,
                ""SBD"": 7.198452,
                ""SCR"": 12.19046,
                ""SDG"": 4.4291,
                ""SEK"": 6.63735,
                ""SGD"": 1.246813,
                ""SHP"": 0.620267,
                ""SLL"": 4299.3335,
                ""SOS"": 1216.600033,
                ""SRD"": 3.283333,
                ""STD"": 18232.000667,
                ""SVC"": 8.73106,
                ""SYP"": 139.884999,
                ""SZL"": 10.16161,
                ""THB"": 31.55623,
                ""TJS"": 4.7713,
                ""TMT"": 2.8535,
                ""TND"": 1.666174,
                ""TOP"": 1.818573,
                ""TRY"": 2.031968,
                ""TTD"": 6.38542,
                ""TWD"": 29.56642,
                ""TZS"": 1605.95,
                ""UAH"": 8.17972,
                ""UGX"": 2511.883333,
                ""USD"": 1,
                ""UYU"": 21.17902,
                ""UZS"": 2172.368171,
                ""VEF"": 6.290825,
                ""VND"": 21069.666667,
                ""VUV"": 94.705,
                ""WST"": 2.312756,
                ""XAF"": 486.013617,
                ""XAG"": 0.04817462,
                ""XAU"": 0.00077497,
                ""XCD"": 2.70148,
                ""XDR"": 0.653927,
                ""XOF"": 486.225998,
                ""XPF"": 88.4294,
                ""YER"": 215.039701,
                ""ZAR"": 10.16878,
                ""ZMK"": 5252.024745,
                ""ZMW"": 5.545083,
                ""ZWL"": 322.355006
              }
            }
        ";
        }
    }
}
