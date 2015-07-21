using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public static string FormatPhoneNumber(this string phoneNumber)
        {
            return phoneNumber == null ? null : Regex.Replace(phoneNumber, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
        }

        public static object GameStatsField(this HtmlHelper htmlHelper, object statInfo, int gameId)
        {
            if (!(statInfo is int))
            {
                return statInfo;
            }

            return htmlHelper.ActionLink(statInfo.ToString(), "Game", "Statistics", new { gameId }, null);
        }

        public static MvcHtmlString TeamLink(this HtmlHelper htmlHelper, string teamName, int teamId, int year)
        {
            var extraParameters = new RouteValueDictionary
                                  {
                                      { "teamId", teamId },
                                      { "year", year == Consts.CurrentYear ? (int?)null : year }
                                  };

            return htmlHelper.ActionLink(teamName, "View", "Team", extraParameters, null);
        }

        public static MvcHtmlString ToLines<T>(this IEnumerable<T> items)
        {
            return MvcHtmlString.Create(string.Join("<br />", items.Where(i => i != null).Select(i => i.ToString())));
        }

        public static MvcHtmlString UrlToLink(this string url)
        {
            return url == null
                       ? null
                       : MvcHtmlString.Create($"<a href=\"{new UriBuilder(url).Uri}\" target=\"_blank\">{url}</a>");
        }
    }
}