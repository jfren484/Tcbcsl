using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Ganss.XSS;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Models;
using Tcbcsl.Presentation.Areas.Admin.Models;

namespace Tcbcsl.Presentation.Helpers
{
    public static class ExtensionMethods
    {
        public static string AsDisplay(this YearEnum year)
        {
            return year == YearEnum.All
                       ? Consts.AllTime
                       : ((int)year).ToString();
        }

        public static string AsRouteParameter(this YearEnum year)
        {
            return year == YearEnum.All
                       ? year.ToString()
                       : ((int)year).ToString();
        }

        public static string AsTitleSuffix(this YearEnum year)
        {
            return year == YearEnum.All
                       ? " - " + Consts.AllTime
                       : ((int)year).AsTitleSuffix();
        }

        public static string AsTitleSuffix(this int year)
        {
            return year == Consts.CurrentYear
                       ? ""
                       : " - " + year;
        }

        public static IEnumerable<T> FilterTeamsForUser<T>(this IEnumerable<T> allEntities, IPrincipal userPrincipal, Func<T, int?> getTeamId)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var tcbcslUser = userManager.FindById(userPrincipal.Identity.GetUserId());

            if (userManager.IsInRole(tcbcslUser.Id, Roles.LeagueCommissioner))
            {
                return allEntities;
            }

            var teamIds = tcbcslUser.AssignedTeams.Select(at => (int?)at.TeamId).ToList();

            return allEntities.Where(n => teamIds.Contains(getTeamId(n)));
        }

        public static object GameStatsField(this HtmlHelper htmlHelper, object statInfo, int gameId)
        {
            if (!(statInfo is int))
            {
                return statInfo;
            }

            return htmlHelper.RouteLink(statInfo.ToString(), new
                                                             {
                                                                 Controller = "Statistics",
                                                                 Action = "StatisticsForGame",
                                                                 GameId = gameId
                                                             }, null);
        }

        public static bool IsTeamIdValidForUser(this IPrincipal userPrincipal, int? teamId)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var tcbcslUser = userManager.FindById(userPrincipal.Identity.GetUserId());

            return userManager.IsInRole(tcbcslUser.Id, Roles.LeagueCommissioner)
                   || tcbcslUser.AssignedTeams.Any(at => at.TeamId == teamId);
        }

        public static string Sanitize(this string htmlString)
        {
            var sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(htmlString);
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

        public static SelectList ToSelectList(this bool? value)
        {
            return new SelectList(new[]
                                  {
                                      new SelectListItem {Value = "True", Text = "Yes"},
                                      new SelectListItem {Value = "False", Text = "No"}
                                  },
                                  "Value",
                                  "Text",
                                  value);
        }

        public static SelectList ToSelectList(this StateEditModel model)
        {
            var stateSelectListItems = model.States
                                            .Select(s => new SelectListItem { Value = s.StateId.ToString(), Text = s.Name })
                                            .ToList();
            stateSelectListItems.Insert(0, new SelectListItem());

            return new SelectList(stateSelectListItems, "Value", "Text", model.StateId);
        }

        public static MvcHtmlString UrlToLink(this string url)
        {
            return url == null
                       ? null
                       : MvcHtmlString.Create($"<a href=\"{new UriBuilder(url).Uri}\" target=\"_blank\">{url}</a>");
        }
    }
}