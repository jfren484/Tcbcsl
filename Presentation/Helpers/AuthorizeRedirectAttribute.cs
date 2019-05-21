using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tcbcsl.Presentation.Helpers
{
    public class AuthorizeRedirectAttribute : AuthorizeAttribute
    {
        // TODO: handle another way?

        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    base.HandleUnauthorizedRequest(filterContext);

        //    if (filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new {Area = "", Controller = "Home", Action = "Unauthorized"}));
        //    }
        //}
    }
}