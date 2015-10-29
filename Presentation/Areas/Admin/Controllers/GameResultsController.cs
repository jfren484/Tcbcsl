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
    [RoutePrefix("GameResults")]
    public class GameResultsController : AdminControllerBase
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

            var model = Mapper.Map<GameResultsEditModel>(teamYear);
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
                                    var model = Mapper.Map<GameResultsEditModel>(gp);
                                    model.ActionUrls = new Dictionary<string, string>
                                                       {
                                                           ["SubmitResults"] = Url.Action("Game", new {id = model.GameParticipantId }),
                                                           ["EnterStats"] = gp.Game.GameStatus.AllowStatistics && gp.TeamYear.KeepsStats
                                                                                ? Url.Action("Game", "Statistics", new {id = model.GameParticipantId})
                                                                                : null
                                                       };

                                    return model;
                                });

            return Json(data);
        }

        #endregion

        #region Game

        [Route("{id:int}")]
        public ActionResult Game(int id)
        {
            var gameParticipant = DbContext.GameParticipants.SingleOrDefault(gp => gp.GameParticipantId == id);
            if (gameParticipant == null || !User.IsTeamIdValidForUser(gameParticipant.TeamYear.TeamId))
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<GameResultsEditModel>(gameParticipant);
            PopulateDropdownLists(model);

            return View(model);
        }

        [HttpPost]
        [Route("{id:int}")]
        public ActionResult Game(int id, GameResultsEditModel model)
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

        private void PopulateDropdownLists(GameResultsEditTeamModel model)
        {
            var teams = DbContext.TeamYears
                                 .Where(ty => ty.TeamId != Consts.TeamTBDTeamId && ty.Year == model.Year && ty.GameParticipants.Any())
                                 .OrderBy(ty => ty.FullName)
                                 .ToList()
                                 .FilterTeamsForUser(User, ty => ty.TeamId);
            model.Teams = Mapper.Map<List<TeamBasicInfoModel>>(teams);
        }

        private void PopulateDropdownLists(GameResultsEditModel model)
        {
        }

        #endregion
    }
}