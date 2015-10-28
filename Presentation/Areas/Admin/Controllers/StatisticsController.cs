using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner + ", " + Roles.TeamCoach)]
    [RouteArea("Admin")]
    [RoutePrefix("Statistics")]
    public class StatisticsController : AdminControllerBase
    {
        #region List

        [Route("")]
        public ActionResult Default()
        {
            if (!UserCache.AssignedTeams.Any() && !User.IsInRole(Roles.LeagueCommissioner))
            {
                return HttpNotFound();
            }


            var teamId = UserCache.AssignedTeams.Any()
                             ? UserCache.AssignedTeams.First().Key
                             : DbContext.TeamYears
                                        .First(ty => ty.Year == Consts.CurrentYear)
                                        .TeamId;

            return RedirectToAction("List", new {Id = teamId});
        }

        [Route("Team/{id:int}/{year:year?}")]
        public ActionResult List(int id, int year = Consts.CurrentYear)
        {
            var teamYear = DbContext.TeamYears.SingleOrDefault(ty => ty.TeamId == id && ty.Year == year);
            if (teamYear == null || !User.IsTeamIdValidForUser(id))
            {
                return HttpNotFound();
            }

            var model = new StatisticsEditScheduleModel
                        {
                            Team = new StatisticsEditTeamModel
                                   {
                                       TeamId = id,
                                       Year = year,
                                       FullName = teamYear.FullName
                                   }
                        };
            PopulateDropdownLists(model.Team);

            return View(model);
        }

        [HttpPost]
        [Route("Data/{id:int}/{year:year?}")]
        public JsonResult Data(int id, int year = Consts.CurrentYear)
        {
            var data = DbContext.GameParticipants
                                .Where(gp => gp.TeamYear.TeamId == id && gp.TeamYear.Year == year)
                                .ToList()
                                .Select(gp =>
                                {
                                    var model = Mapper.Map<StatisticsEditScheduleModel>(gp);
                                    model.EnterStatsUrl = Url.Action("Game", new { id = model.GameParticipantId });

                                    return model;
                                });

            return Json(data);
        }

        #endregion

        #region Game

        [Route("Game/{id:int}")]
        public ActionResult Game(int id)
        {
            var gameParticipant = DbContext.GameParticipants.SingleOrDefault(gp => gp.GameParticipantId == id);
            if (gameParticipant == null || !User.IsTeamIdValidForUser(gameParticipant.TeamYear.TeamId))
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<TeamEditModel>(gameParticipant);
            PopulateDropdownLists(model);

            return View(model);
        }

        [HttpPost]
        [Route("Game/{id:int}")]
        public ActionResult Game(int id, TeamEditModel model)
        {
            var gameParticipant = DbContext.GameParticipants.SingleOrDefault(gp => gp.GameParticipantId == id);
            if (gameParticipant == null || !User.IsTeamIdValidForUser(gameParticipant.TeamYear.TeamId))
            {
                return HttpNotFound();
            }

            Mapper.Map(model, gameParticipant);

            DbContext.SaveChanges(User.Identity.Name);

            return RedirectToAction("List");
        }

        #endregion

        #region Helpers

        private void PopulateDropdownLists(StatisticsEditTeamModel model)
        {
            var teams = DbContext.TeamYears
                                 .Where(ty => ty.Year == model.Year && ty.KeepsStats && ty.GameParticipants.Any())
                                 .OrderBy(ty => ty.FullName)
                                 .ToList()
                                 .FilterTeamsForUser(User, ty => ty.TeamId);
            model.Teams = Mapper.Map<List<TeamBasicInfoModel>>(teams);
        }

        private void PopulateDropdownLists(TeamEditModel model)
        {
            var divisions = DbContext.DivisionYears
                                     .Where(dy => dy.Year == Consts.CurrentYear)
                                     .OrderBy(dy => dy.Sort)
                                     .Select(dy => new SelectListItem {Value = dy.DivisionYearId.ToString(), Text = dy.Name})
                                     .ToList();
            model.Division.ItemSelectList = new SelectList(divisions, "Value", "Text", model.Division.DivisionYearId);

            var clinchItems = Consts.ClinchDescriptions
                                    .Select(kvp => new SelectListItem { Value = kvp.Key.ToString(), Text = kvp.ClinchDescriptionFormatted() })
                                    .ToList();
            clinchItems.Insert(0, new SelectListItem { Text = "(none)" });
            model.Clinch.ItemSelectList = new SelectList(clinchItems, "Value", "Text", model.Clinch.ClinchChar);
        }

        #endregion
    }
}