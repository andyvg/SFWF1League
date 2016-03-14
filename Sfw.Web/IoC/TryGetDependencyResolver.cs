using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace Sfw.Web.IoC
{
    public class TryGetDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            try
            {
                return ServiceLocator.Current.GetInstance(serviceType);
            }
            catch (ActivationException)
            {
                return null;
            }
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return ServiceLocator.Current.GetAllInstances(serviceType);
            }
            catch (ActivationException)
            {
                return null;
            }
        }
    }
}