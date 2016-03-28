using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Sfw.Racing.DataRepository;
using Sfw.Racing.DataRepository.Core;
using Sfw.Web.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sfw.Racing.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Repository.Register();
            RegisterContainer();
        }

        private void RegisterContainer()
        {
            UnityServiceLocator locator = new UnityServiceLocator(ConfigureUnityContainer());
            ServiceLocator.SetLocatorProvider(() => locator);

            DependencyResolver.SetResolver(new TryGetDependencyResolver());
        }

        private static IUnityContainer ConfigureUnityContainer()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType<IConnectionFactory, Sfw.Racing.DataRepository.SqlConnectionFactory>();
            container.RegisterType<IRepository, Sfw.Racing.DataRepository.Repository>();
            container.RegisterType<ICacheManager, Sfw.Racing.DataRepository.CacheManager>();

            return container;
        }
    }
}
