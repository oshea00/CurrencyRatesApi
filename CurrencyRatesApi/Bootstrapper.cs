using System.Web.Http;
using System.Web.Mvc;
using CurrencyRatesApi.Repositories;
using Microsoft.Practices.Unity;


namespace CurrencyRatesApi
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new Unity.Mvc3.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IExchangeRatesRepository, ExchangeRatesRepository>(new ContainerControlledLifetimeManager());
            //container.RegisterType<IUserProvider, UserProvider>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}