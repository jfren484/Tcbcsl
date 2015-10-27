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

        [Route("{id:int?}/{year:year?}")]
        public ActionResult List(int? id, int year = Consts.CurrentYear)
        {
            var teamId = id ?? (UserCache.AssignedTeams.Any()
                                    ? UserCache.AssignedTeams.First().Key
                                    : DbContext.TeamYears
                                               .First(ty => ty.Year == year)
                                               .TeamId);

            if (!User.IsTeamIdValidForUser(teamId))
            {
                return HttpNotFound();
            }

            return View(new StatisticsEditScheduleModel {TeamId = teamId, Year = year});
        }

        //[HttpPost]
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

            return Json(data, JsonRequestBehavior.AllowGet);
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

        private void PopulateDropdownLists(TeamEditModel model)
        {
            var divisions = DbContext.DivisionYears
                                     .Where(dy => dy.Year == Consts.CurrentYear)
                                     .OrderBy(dy => dy.Sort)
                                     .Select(dy => new SelectListItem {Value = dy.DivisionYearId.ToString(), Text = dy.Name})
                                     .ToList();
            model.Division.ItemSelectList = new SelectList(divisions, "Value", "Text", model.Division.DivisionYearId);

            var clinchItems = Consts.ClinchDescriptions
                                    .Select(kvp => new SelectListItem { Value = kvp.Key.ToString(), Text = $"{kvp.Key} - Clinched {kvp.Value}" })
                                    .ToList();
            clinchItems.Insert(0, new SelectListItem { Text = "(none)" });
            model.Clinch.ItemSelectList = new SelectList(clinchItems, "Value", "Text", model.Clinch.ClinchChar);
        }

        #endregion
    }
}