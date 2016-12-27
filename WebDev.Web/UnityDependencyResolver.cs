using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;


namespace WebDev.Web
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer unity;

        public UnityDependencyResolver(IUnityContainer unity)
        {
            this.unity = unity;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return this.unity.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                // By definition of IDependencyResolver contract, this should return null if it cannot be found.
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.unity.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                // By definition of IDependencyResolver contract, this should return null if it cannot be found.
                return null;
            }
        }
    }
}