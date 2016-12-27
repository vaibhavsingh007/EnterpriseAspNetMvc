using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebDev.Data.Base;
using WebDev.DomainModel;

namespace WebDev.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static IUnityContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            LoadGlobalVariables();
            InitializeDependencyInjectionContainer();
            InitializeDatabase();
        }

        /// <summary>
        /// Initialize Dependency Injection
        /// </summary>
        private static void InitializeDependencyInjectionContainer()
        {
            container = new UnityContainerFactory().CreateConfiguredContainer();
            var serviceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        /// <summary>
        /// Initialize Database
        /// </summary>
        private static void InitializeDatabase()
        {
            var repositoryInitializer = ServiceLocator.Current.GetInstance<IRepositoryInitializer>();
            repositoryInitializer.Initialize();
        }

        /// <summary>
        /// Load application level variables. Usually from config.
        /// </summary>
        private void LoadGlobalVariables()
        {
            Constants.LogDir = Server.MapPath(ConfigurationManager.AppSettings["LogDir"]);
        }
    }
}