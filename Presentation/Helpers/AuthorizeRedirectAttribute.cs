using System.Web.Mvc;
using System.Web.Routing;

namespace Tcbcsl.Presentation.Helpers
{
    public class AuthorizeRedirectAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            if (filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new {Area = "", Controller = "Home", Action = "Unauthorized"}));
            }
        }
    }
}