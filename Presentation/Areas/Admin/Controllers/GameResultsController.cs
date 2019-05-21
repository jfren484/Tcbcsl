using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner + ", " + Roles.TeamCoach)]
    public class GameResultsController : AdminControllerBase
    {
        #region List

        public ActionResult List()
        {
            if (!AssignedTeams.Any() && !User.IsInRole(Roles.LeagueCommissioner))
            {
                return NotFound();
            }

            var teamId = AssignedTeams.Any()
                             ? AssignedTeams.First().Key
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
                return NotFound();
            }

            var model = Mapper.Map<GameResultsListModel>(teamYear);
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
                                    var model = Mapper.Map<GameResultsListModel>(gp);

                                    model.UrlsForActions = new Dictionary<string, string>
                                                       {
                                                           ["SubmitResults"] = User.IsInRole(Roles.LeagueCommissioner)
                                                               ? Url.Action("Game", new {id = model.GameParticipantId, reportingTeamId = id})
                                                               : Url.Action("Game", new {id = model.GameParticipantId}),
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
        public ActionResult Game(int id, int? reportingTeamId = null)
        {
            var gameParticipant = DbContext.GameParticipants.SingleOrDefault(gp => gp.GameParticipantId == id);
            if (gameParticipant == null || !User.IsTeamIdValidForUser(gameParticipant.TeamYear.TeamId))
            {
                return NotFound();
            }

            var model = Mapper.Map<GameResultsEditModel>(gameParticipant.Game);
            PopulateDropdownLists(model.NewReport, gameParticipant.Game, reportingTeamId);

            return View(model);
        }

        [HttpPost]
        [Route("{id:int}")]
        public ActionResult Game(int id, GameResultsEditModel model)
        {
            var gameParticipant = DbContext.GameParticipants.SingleOrDefault(gp => gp.GameParticipantId == id);
            if (gameParticipant == null || !User.IsTeamIdValidForUser(gameParticipant.TeamYear.TeamId))
            {
                return NotFound();
            }

            var newReport = Mapper.Map<GameResultReport>(model.NewReport);
            gameParticipant.Game.GameResultReports.Add(newReport);
            DbContext.SaveChanges(User.Identity.GetUserId());

            if (gameParticipant.Game.IsFinalized)
            {
                return Redirect(model.NewReport.UrlForReturn);
            }

            if (!model.NewReport.IsConfirmation)
            {
                if (model.NewReport.GameStatus.GameStatusId != null)
                {
                    gameParticipant.Game.GameStatusId = model.NewReport.GameStatus.GameStatusId.Value;
                }
                gameParticipant.Game.RoadParticipant.RunsScored = model.NewReport.RoadParticipant.RunsScored;
                gameParticipant.Game.HomeParticipant.RunsScored = model.NewReport.HomeParticipant.RunsScored;
                DbContext.SaveChanges(User.Identity.GetUserId());

                // TODO: email users for other team
            }
            else if (CanFinalize(gameParticipant.Game))
            {
                gameParticipant.Game.IsFinalized = true;
                DbContext.SaveChanges(User.Identity.GetUserId());
            }

            return Redirect(model.NewReport.UrlForReturn);
        }

        #endregion

        #region Helpers

        private bool CanFinalize(Game game)
        {
            var reportData = game.GameResultReports
                                 .OrderByDescending(r => r.Created)
                                 .Select(r => new
                                              {
                                                  r.IsConfirmation,
                                                  r.TeamId
                                              })
                                 .ToList();

            var reportingTeamIds = reportData.Take(reportData.FindIndex(d => !d.IsConfirmation) + 1)
                                             .Where(d => d.TeamId.HasValue)
                                             .Select(d => d.TeamId.Value)
                                             .ToList();

            return game.GameParticipants
                       .Select(gp => gp.TeamYear.TeamId)
                       .ToList()
                       .All(reportingTeamIds.Contains);
        }

        private void PopulateDropdownLists(TeamPickerModel model)
        {
            var teams = DbContext.TeamYears
                                 .Where(ty => ty.TeamId != Consts.TeamTBDTeamId && ty.Year == model.Year && ty.GameParticipants.Any())
                                 .OrderBy(ty => ty.FullName)
                                 .ToList()
                                 .FilterTeamsForUser(User, ty => ty.TeamId);
            model.Teams = Mapper.Map<List<TeamBasicInfoModel>>(teams);
        }

        private void PopulateDropdownLists(GameResultsEditCreateReportModel model, Game game, int? teamId)
        {
            var teams = game.GameParticipants
                            .Select(gp => gp.TeamYear)
                            .OrderBy(ty => ty.FullName);

            var teamModels = new[] { new TeamBasicInfoModel { FullName = Consts.LeagueNameForList } }
                                 .Concat(Mapper.Map<List<TeamBasicInfoModel>>(teams))
                                 .FilterTeamsForUser(User, m => m.TeamId)
                                 .ToList();
            model.Team.TeamId = teamModels.Count == 1 ? teamModels[0].TeamId : teamId;
            model.Team.ItemSelectList = new SelectList(teamModels, "TeamId", "FullName", model.Team.TeamId);

            model.GameStatus.ItemSelectList = GetGameStatusesSelectListItems(null, true);
        }

        #endregion
    }
}