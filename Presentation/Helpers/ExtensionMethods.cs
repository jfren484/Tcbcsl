using Ganss.XSS;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Tcbcsl.Data.Entities;
using Tcbcsl.Data.Identity;
using Tcbcsl.Presentation.Models;

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

        public static string AsRouteParameter(this int year)
        {
            return year == Consts.CurrentYear
                       ? null
                       : year.ToString();
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

        public static string ClinchDescriptionFormatted(this KeyValuePair<string, string> kvp)
        {
            return $"{kvp.Key} - Clinched {kvp.Value}";
        }

        public static IEnumerable<T> AddSeasonToDateStats<T>(this IEnumerable<T> stats) where T : BaseStatisticsRowModel
        {
            int hits = 0, walks = 0, totalBases = 0, sacrificeFlies = 0;
            double atBats = 0;

            foreach (var statsModel in stats.OrderBy(s => s.Year).ToList())
            {
                hits += statsModel.Hits;
                walks += statsModel.Walks;
                totalBases += statsModel.TotalBases;
                sacrificeFlies += statsModel.SacrificeFlies;
                atBats += statsModel.AtBats;

                statsModel.ToDateAverage = atBats == 0 ? 0 : hits / atBats;
                statsModel.ToDateOnBasePercentage = (atBats + walks + sacrificeFlies) == 0 ? 0 : (hits + walks) / (atBats + walks + sacrificeFlies);
                statsModel.ToDateSluggingPercentage = atBats == 0 ? 0 : totalBases / atBats;
                statsModel.ToDateOnBasePlusSlugging = statsModel.ToDateOnBasePercentage + statsModel.ToDateSluggingPercentage;
            }

            return stats;
        }

        public static IEnumerable<T> FilterTeamsForUser<T>(this IEnumerable<T> allEntities, IPrincipal userPrincipal, Func<T, int?> getTeamId)
        {
            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var tcbcslUser = userManager.FindById(userPrincipal.Identity.GetUserId());

            //if (userManager.IsInRole(tcbcslUser.Id, Roles.LeagueCommissioner))
            //{
            //    return allEntities;
            //}

            //var teamIds = tcbcslUser.AssignedTeams.Select(at => (int?)at.TeamId).ToList();

            //return allEntities.Where(n => teamIds.Contains(getTeamId(n)));

            // TODO: handle this
            return allEntities;
        }

        public static object GameStatsField(this IHtmlHelper htmlHelper, object statInfo, int gameId)
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

        public static string GetUserId(this IIdentity identity)
        {
            throw new NotImplementedException();
        }

        public static bool IsTeamIdValidForUser(this IPrincipal userPrincipal, int? teamId)
        {
            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var tcbcslUser = userManager.FindById(userPrincipal.Identity.GetUserId());

            //return userManager.IsInRole(tcbcslUser.Id, Roles.LeagueCommissioner)
            //       || teamId == Consts.PlayerPoolTeamId
            //       || tcbcslUser.AssignedTeams.Any(at => at.TeamId == teamId);

            // TODO: handle this
            return true;
        }

        public static bool IsUserInRole(this IPrincipal userPrincipal, string role)
        {
            //return HttpContext.Current
            //                  .GetOwinContext()
            //                  .GetUserManager<ApplicationUserManager>()
            //                  .IsInRole(userPrincipal.Identity.GetUserId(), role);

            // TODO: handle this
            return true;
        }

        public static TcbcslUser TcbcslUser(this IPrincipal userPrincipal)
        {
            //return HttpContext.Current
            //                  .GetOwinContext()
            //                  .GetUserManager<ApplicationUserManager>()
            //                  .FindByName(userPrincipal.Identity.Name);

            // TODO: handle this
            return new TcbcslUser();
        }

        public static string Sanitize(this string htmlString)
        {
            var sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(htmlString);
        }

        public static IHtmlContent TeamLink(this IHtmlHelper htmlHelper, string teamName, int teamId, int year)
        {
            var extraParameters = new Dictionary<string, int?>
                                  {
                                      { "teamId", teamId },
                                      { "year", year == Consts.CurrentYear ? (int?)null : year }
                                  };

            return htmlHelper.ActionLink(teamName, "View", "Team", extraParameters, null);
        }

        public static IHtmlContent ToLines<T>(this IEnumerable<T> items)
        {
            return new HtmlString(string.Join("<br />", items.Where(i => i != null).Select(i => i.ToString())));
        }

        public static HtmlString UrlToLink(this string url)
        {
            return url == null
                       ? null
                       : new HtmlString($"<a href=\"{new UriBuilder(url).Uri}\" target=\"_blank\">{url}</a>");
        }
    }
}