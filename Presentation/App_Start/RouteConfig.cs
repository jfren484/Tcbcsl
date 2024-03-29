﻿using System.Web.Mvc;
using System.Web.Mvc.Routing;
using System.Web.Routing;
using Tcbcsl.Presentation.Helpers;

namespace Tcbcsl.Presentation
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var constraintsResolver = new DefaultInlineConstraintResolver();
            constraintsResolver.ConstraintMap.Add("year", typeof(NumericYearRouteConstraint));
            constraintsResolver.ConstraintMap.Add("years", typeof(FlexYearRouteConstraint));
            routes.MapMvcAttributeRoutes(constraintsResolver);
        }
    }
}
