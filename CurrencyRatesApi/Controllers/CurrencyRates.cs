using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CurrencyRatesApi.Domain;
using CurrencyRatesApi.Repositories;

namespace CurrencyRatesApi.Controllers
{
    public class CurrencyRatesController : ApiController
    {
        IExchangeRatesRepository _repo;
        public CurrencyRatesController(IExchangeRatesRepository repo)
        {
            _repo = repo;
        }


        public HttpResponseMessage Get()
        {
            return Request.CreateResponse<ExchangeRates>(HttpStatusCode.OK, _repo.GetCurrentRates());
        }

        public HttpResponseMessage Get(DateTime effectiveDate)
        {
            return Request.CreateResponse<ExchangeRates>(HttpStatusCode.OK, _repo.GetHistoricalRates(effectiveDate));
        }
    }
}
