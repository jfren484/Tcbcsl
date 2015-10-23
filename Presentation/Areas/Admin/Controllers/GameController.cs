using System;
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
    [RoutePrefix("Game")]
    public class GameController : AdminControllerBase
    {
        #region Create/Edit

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("Create")]
        public ActionResult Create(string returnUrl, DateTime date)
        {
            var model = new GameEditModel
                        {
                            GameDate = date,
                            GameType = new GameEditTypeModel(),
                            GameStatus = new GameEditStatusModel(),
                            RoadTeam = new GameParticipantEditModel {TeamYear = new GameEditTeamModel()},
                            HomeTeam = new GameParticipantEditModel {TeamYear = new GameEditTeamModel()},
                            ReturnUrl = returnUrl
                        };
            PopulateDropdownLists(model);

            return View("Edit", model);
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(GameEditModel model)
        {
            var roadTeam = Mapper.Map<GameParticipant>(model.RoadTeam);
            var homeTeam = Mapper.Map<GameParticipant>(model.HomeTeam);
            homeTeam.IsHost = true;

            var game = Mapper.Map<Game>(model);
            game.GameParticipants = new[] {roadTeam, homeTeam};

            DbContext.Games.Add(game);
            DbContext.SaveChanges(User.Identity.Name);

            return Redirect(model.ReturnUrl);
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, string returnUrl)
        {
            var game = DbContext.Games.SingleOrDefault(g => g.GameId == id);
            if (game == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<GameEditModel>(game);
            model.ReturnUrl = returnUrl;
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
            Mapper.Map(model.RoadTeam, game.GameParticipants.Single(gp => !gp.IsHost));
            Mapper.Map(model.HomeTeam, game.GameParticipants.Single(gp => gp.IsHost));

            DbContext.SaveChanges(User.Identity.Name);

            return Redirect(model.ReturnUrl);
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

            DbContext.SaveChanges(User.Identity.Name);

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

            DbContext.SaveChanges(User.Identity.Name);

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

            DbContext.SaveChanges(User.Identity.Name);

            return HttpOk();
        }

        #endregion

        #region Helpers

        private void PopulateDropdownLists(GameEditModel model)
        {
            var gameStatuses = DbContext.GameStatuses
                                        .OrderBy(gs => gs.GameStatusId)
                                        .Select(gs => new SelectListItem {Value = gs.GameStatusId.ToString(), Text = gs.Description})
                                        .ToList();
            model.GameStatus.ItemSelectList = new SelectList(gameStatuses, "Value", "Text", model.GameStatus.GameStatusId);

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
            model.RoadTeam.TeamYear.ItemSelectList = new SelectList(teamYears, "Value", "Text", model.RoadTeam.TeamYear.TeamYearId);
            model.HomeTeam.TeamYear.ItemSelectList = new SelectList(teamYears, "Value", "Text", model.HomeTeam.TeamYear.TeamYearId);
        }

        #endregion
    }
}