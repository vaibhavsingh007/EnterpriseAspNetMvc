using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDev.Web.UnityExtensions
{
    public class HttpContextPerRequestStore : IPerRequestStore
    {
        public HttpContextPerRequestStore()
        {
            if (HttpContext.Current.ApplicationInstance != null)
            {
                // Note: We'd like to do this, but you cannot sign up for the EndRequest from
                // from this application instance as it is actually different than the one the
                // the EndRequest handler is actually invoked from.
                //HttpContext.Current.ApplicationInstance.EndRequest += this.EndRequestHandler;
            }
        }

        public object GetValue(object key)
        {
            return HttpContext.Current.Items[key];
        }

        public void SetValue(object key, object value)
        {
            HttpContext.Current.Items[key] = value;
        }

        public void RemoveValue(object key)
        {
            HttpContext.Current.Items.Remove(key);
        }

        private void EndRequestHandler(object sender, EventArgs e)
        {
            EventHandler handler = this.EndRequest;
            if (handler != null) handler(this, e);
        }

        public event EventHandler EndRequest;
    }
}