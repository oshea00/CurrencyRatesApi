using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace CurrencyRatesApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "CurrencyRates",
                routeTemplate: "api/CurrencyRates/{id}",
                defaults: new { controller = "CurrencyRates", id = RouteParameter.Optional }
            );
        }
    }
}
