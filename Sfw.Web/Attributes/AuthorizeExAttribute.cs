using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace Sfw.Web.Attributes
{
    /// <summary>
    /// An extension to the System.Web.Mvc.AuthorizeAttribute that returns a 403 status code
    /// (Forbidden) if the request is authenticated but not authorized.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeExAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                // Return 403 status code (Forbidden) if the user is authenticated but not authorized.
                filterContext.Result = new System.Web.Mvc.HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            else
            {
                // Let the base handle the request.
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}