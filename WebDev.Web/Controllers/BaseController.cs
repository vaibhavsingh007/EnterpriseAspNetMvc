using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using System.Web.Routing;

namespace WebDev.Web.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Service Locator Variable
        /// </summary>
        private readonly IServiceLocator _serviceLocator;

        /// <summary>
        /// Base constructor should be called from every inheriting controller with service locator
        /// </summary>
        /// <param name="serviceLocator"></param>
        public BaseController(IServiceLocator serviceLocator)
        {
            this._serviceLocator = serviceLocator;
        }

        /// <summary>
        /// override controller Initialize method to pass applcation id from query string to the View bag and use the same while saving the data
        /// </summary>
        /// <param name="rc"></param>
        protected override void Initialize(RequestContext rc)
        {
            base.Initialize(rc);
            //string UserName = "Guest";
            //User loginUser = null;        // TODO: Use this when implementing 'User'.

            //if (HttpContext != null && HttpContext != null)
            //{
            //    if (Request.IsAuthenticated)
            //    {
            //        loginUser = Using<Users>().Execute(User.Identity.Name);
            //        UserName = loginUser.UserName;
            //    }
            //}

            //ViewBag.UserName = UserName;
        }

        /// <summary>
        /// used to map the service location of the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T Using<T>() where T : class
        {
            var handler = _serviceLocator.GetInstance<T>();
            if (handler == null)
            {
                throw new NullReferenceException("Unable to resolve type with service locator; type " + typeof(T).Name);
            }
            return handler;
        }
    }
}