using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;
using Microsoft.AspNet.Identity;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner + ", " + Roles.TeamCoach)]
    [RouteArea("Admin")]
    [RoutePrefix("Game")]
    public class GameController : AdminControllerBase
    {
        #region Create/Edit

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("Create")]
        public ActionResult Create(string urlForReturn, DateTime date)
        {
            var model = new GameEditModel
                        {
                            GameDate = date + TimeSpan.FromHours(date.DayOfWeek == DayOfWeek.Saturday ? 8 : 18.5),
                            GameType = new GameEditTypeModel(),
                            GameStatus = new GameEditStatusModel(),
                            RoadParticipant = new GameParticipantEditModel {TeamYear = new GameEditTeamModel()},
                            HomeParticipant = new GameParticipantEditModel {TeamYear = new GameEditTeamModel()},
                            UrlForReturn = urlForReturn
                        };
            PopulateDropdownLists(model);

            return View("Edit", model);
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(GameEditModel model)
        {
            var roadTeam = Mapper.Map<GameParticipant>(model.RoadParticipant);
            var homeTeam = Mapper.Map<GameParticipant>(model.HomeParticipant);
            homeTeam.IsHost = true;

            var game = Mapper.Map<Game>(model);
            game.GameParticipants = new[] {roadTeam, homeTeam};

            DbContext.Games.Add(game);
            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, string urlForReturn)
        {
            var game = DbContext.Games.SingleOrDefault(g => g.GameId == id);
            if (game == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<GameEditModel>(game);
            model.UrlForReturn = urlForReturn;
            PopulateDropdownLists(model);

            return View(model);
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, GameEditModel model)
        {
            var game = DbContext.Games.SingleOrDefault(g => g.GameId == id);
            if (game == null)
            {
                return HttpNotFound();
            }

            Mapper.Map(model, game);
            Mapper.Map(model.RoadParticipant, game.RoadParticipant);
            Mapper.Map(model.HomeParticipant, game.HomeParticipant);

            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        #endregion

        #region Update

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("Forfeit/{winnerGameParticipantId:int}")]
        public ActionResult UpdateGameAsForfeit(int winnerGameParticipantId)
        {
            var gameParticipant = DbContext.GameParticipants
                                           .SingleOrDefault(gp => gp.GameParticipantId == winnerGameParticipantId);
            if (gameParticipant == null)
            {
                return HttpNotFound();
            }

            gameParticipant.Game.GameStatusId = GameStatus.Forfeited;
            gameParticipant.RunsScored = 15;

            DbContext.SaveChanges(User.Identity.GetUserId());

            return HttpOk();
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("Postpone/{gameId:int}")]
        public ActionResult UpdateGameAsPostponed(int gameId)
        {
            var game = DbContext.Games
                                .SingleOrDefault(g => g.GameId == gameId);
            if (game == null)
            {
                return HttpNotFound();
            }

            game.GameStatusId = GameStatus.Postponed;

            DbContext.SaveChanges(User.Identity.GetUserId());

            return HttpOk();
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("RainedOut/{gameId:int}")]
        public ActionResult UpdateGameAsRainedOut(int gameId)
        {
            var game = DbContext.Games
                                .SingleOrDefault(g => g.GameId == gameId);
            if (game == null)
            {
                return HttpNotFound();
            }

            game.GameStatusId = GameStatus.RainedOut;

            DbContext.SaveChanges(User.Identity.GetUserId());

            return HttpOk();
        }

        #endregion

        #region Helpers

        private void PopulateDropdownLists(GameEditModel model)
        {
            model.GameStatus.ItemSelectList = GetGameStatusesSelectListItems(model.GameStatus.GameStatusId);

            var gameTypes = DbContext.GameTypes
                                     .OrderBy(gt => gt.GameTypeId)
                                     .Select(gt => new SelectListItem {Value = gt.GameTypeId.ToString(), Text = gt.Description})
                                     .ToList();
            model.GameType.ItemSelectList = new SelectList(gameTypes, "Value", "Text", model.GameType.GameTypeId);

            var teamYears = DbContext.TeamYears
                                     .Where(ty => ty.Year == model.GameDate.Year)
                                     .OrderBy(ty => ty.FullName)
                                     .Select(ty => new SelectListItem {Value = ty.TeamYearId.ToString(), Text = ty.FullName})
                                     .ToList();
            model.RoadParticipant.TeamYear.ItemSelectList = new SelectList(teamYears, "Value", "Text", model.RoadParticipant.TeamYear.TeamYearId);
            model.HomeParticipant.TeamYear.ItemSelectList = new SelectList(teamYears, "Value", "Text", model.HomeParticipant.TeamYear.TeamYearId);
        }

        #endregion
    }
}