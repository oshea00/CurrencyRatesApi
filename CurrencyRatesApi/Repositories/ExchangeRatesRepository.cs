using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using CurrencyRatesApi.Domain;
using ServiceStack.Text;

namespace CurrencyRatesApi.Repositories
{
    public interface IExchangeRatesRepository 
    {
        ExchangeRates GetCurrentRates();
        ExchangeRates GetHistoricalRates(DateTime date);
    }


    public class ExchangeRatesRepository : IExchangeRatesRepository
    {
        string _uriPath;
        string _appId;

        public ExchangeRatesRepository()
        {
            _uriPath = ConfigurationManager.AppSettings.Get("OpenExchangeRatesUri");
            _appId = ConfigurationManager.AppSettings.Get("OpenExchangeRatesAppId");
        }

        private string GetJsonFromUrl(Uri url)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Proxy = WebRequest.GetSystemWebProxy();
            request.UseDefaultCredentials = true;
            var response = (HttpWebResponse)request.GetResponse();
            var json = "";

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var reader = new StreamReader(response.GetResponseStream());
                json = reader.ReadToEnd();
                reader.Close();
            }

            response.Close();
            return json;
        }

        private string GetCurrentRatesJson()
        {
            var uri = new Uri(_uriPath + "/latest.json?app_id=" + _appId);
            return GetJsonFromUrl(uri);
        }

        private string GetHistoricalRatesJson(DateTime date)
        {
            var dateString = date.ToString("yyyy-MM-dd");
            var uri = new Uri(_uriPath + String.Format("/historical/{0}.json?app_id=",dateString) + _appId);
            return GetJsonFromUrl(uri);
        }

        private ExchangeRates ParseJson(string json)
        {
            ExchangeRates rates = null;
            if (!String.IsNullOrEmpty(json))
            {
                rates = JsonObject.Parse(json)
                .ConvertTo<ExchangeRates>(x =>
                    new ExchangeRates
                    {
                        EffectiveDate = x.JsonTo<double>("timestamp").DateTimeFromUnix(),
                        BaseCurrencyCode = x.Get("base"),
                        Rates = ParseRates(x),
                    }
                );
            }
            return rates;
        }

        private List<Rate> ParseRates(JsonObject j)
        {
            var list = new List<Rate>();
            var ratesDict = j.JsonTo<Dictionary<string, decimal>>("rates");
            foreach (var currency in ratesDict.Keys)
            {
                list.Add(new Rate { CurrencyCode = currency, ConversionRate = ratesDict[currency] });
            }
            return list;
        }

        public ExchangeRates GetCurrentRates()
        {
            var json = GetCurrentRatesJson();
            return ParseJson(json);
        }

        public ExchangeRates GetHistoricalRates(DateTime date)
        {
            var json = GetHistoricalRatesJson(date);
            return ParseJson(json);
        }
    }
}
