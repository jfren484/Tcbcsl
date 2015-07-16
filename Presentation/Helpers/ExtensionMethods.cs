using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Tcbcsl.Presentation.Helpers
{
    public static class ExtensionMethods
    {
        public static string AsTitleSuffix(this int year)
        {
            return year == Consts.CurrentYear
                ? ""
                : " - " + year;
        }

        public static MvcHtmlString TeamLink(this HtmlHelper htmlHelper, string teamName, int teamId, int year)
        {
            var extraParameters = new RouteValueDictionary { { "teamId", teamId } };
            if (year != Consts.CurrentYear)
            {
                extraParameters.Add("year", year);
            }

            return htmlHelper.ActionLink(teamName, "View", "Team", extraParameters, null);
        }
    }
}