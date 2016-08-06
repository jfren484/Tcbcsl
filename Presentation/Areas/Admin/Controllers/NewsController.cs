using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;
using Microsoft.AspNet.Identity;
using Tcbcsl.Data;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner + ", " + Roles.TeamCoach)]
    [RouteArea("Admin")]
    [RoutePrefix("News")]
    public class NewsController : AdminControllerBase
    {
        #region List

        [Route("")]
        public ActionResult List()
        {
            return View(new NewsEditModel
            {
                Team = new NewsEditTeamModel { Teams = GetTeams(Consts.CurrentYear) }
            });
        }

        [HttpPost]
        [Route("Data")]
        public JsonResult Data()
        {
            var data = DbContext.NewsItems
                                .FilterTeamsForUser(User, n => n.TeamId)
                                .OrderByDescending(n => n.Created)
                                .ToList()
                                .Select(n =>
                                        {
                                            var model = Mapper.Map<NewsEditModel>(n);
                                            model.UrlForEdit = Url.Action("Edit", new {id = model.NewsItemId});

                                            return model;
                                        });

            return Json(data);
        }

        #endregion

        #region Create/Edit

        [Route("Create")]
        public ActionResult Create()
        {
            var model = new NewsEditModel
                        {
                            IsActive = true,
                            StartDate = CentralTimeZone.Now,
                            EndDate = CentralTimeZone.Today.AddDays(14),
                            Team = new NewsEditTeamModel {Teams = GetTeams(Consts.CurrentYear)}
                        };

            if (model.Team.Teams.Count == 1)
            {
                model.Team.TeamId = model.Team.Teams[0].TeamId;
            }

            return View("Edit", model);
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(NewsEditModel model)
        {
            if (!User.IsTeamIdValidForUser(model.Team.TeamId))
            {
                return HttpNotFound();
            }

            var newsItem = Mapper.Map<NewsItem>(model);
            DbContext.NewsItems.Add(newsItem);
            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var newsItem = DbContext.NewsItems.SingleOrDefault(n => n.NewsItemId == id);
            if (newsItem == null || !User.IsTeamIdValidForUser(newsItem.TeamId))
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<NewsEditModel>(newsItem);
            model.Team.Teams = GetTeams(newsItem.Created.Year);

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, NewsEditModel model)
        {
            var newsItem = DbContext.NewsItems.SingleOrDefault(n => n.NewsItemId == id);
            if (newsItem == null || !User.IsTeamIdValidForUser(model.Team.TeamId))
            {
                return HttpNotFound();
            }

            Mapper.Map(model, newsItem);
            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        #endregion

        #region Helpers

        private List<TeamBasicInfoModel> GetTeams(int year)
        {
            var teams = DbContext.TeamYears
                                 .Where(ty => ty.Year == year && ty.DivisionYear.IsInLeague)
                                 .OrderBy(ty => ty.FullName);

            return new[] {new TeamBasicInfoModel {FullName = Consts.LeagueNameForList}}
                .Concat(Mapper.Map<List<TeamBasicInfoModel>>(teams))
                .FilterTeamsForUser(User, n => n.TeamId)
                .ToList();
        }

        #endregion
    }
}