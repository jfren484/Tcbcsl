using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner + ", " + Roles.TeamCoach)]
    [RouteArea("Admin")]
    [RoutePrefix("Player")]
    public class PlayerController : AdminControllerBase
    {
        #region List

        [Route("")]
        public ActionResult List()
        {
            var model = new PlayerEditModel
            {
                Team = new PlayerEditTeamModel(),
                UserManagesMultipleTeams = DbContext.Teams
                                   .Select(t => t.TeamId)
                                   .FilterTeamsForUser(User, id => id)
                                   .Count() > 1
            };
            PopulateDropdownLists(model);

            return View(model);
        }

        [HttpPost]
        [Route("Data")]
        public JsonResult Data()
        {
            var data = SelectPlayerEditModels(DbContext.Players
                                                       .ToList()
                                                       .FilterTeamsForUser(User, p => p.CurrentTeamId));

            return Json(data);
        }

        [Route("Pool")]
        public ActionResult PoolList()
        {
            var model = new PlayerEditModel {Team = new PlayerEditTeamModel()};

            if (Request.Cookies["lastTeamId"] != null)
            {
                model.Team.TeamId = int.Parse(Request.Cookies["lastTeamId"].Value);
            }

            PopulateDropdownLists(model);

            return View(model);
        }

        [HttpPost]
        [Route("Pool/Data")]
        public JsonResult PoolData()
        {
            var data = SelectPlayerEditModels(DbContext.Players
                                                       .Where(p => p.CurrentTeamId == Consts.PlayerPoolTeamId)
                                                       .ToList());

            return Json(data);
        }

        #endregion

        #region Create/Edit

        [Route("Create")]
        public ActionResult Create()
        {
            var model = new PlayerEditModel
                        {
                            Team = new PlayerEditTeamModel(),
                            IsActive = true
                        };
            PopulateDropdownLists(model);

            return View("Edit", model);
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(PlayerEditModel model)
        {
            var player = Mapper.Map<Player>(model);

            DbContext.Players.Add(player);
            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var player = DbContext.Players.SingleOrDefault(p => p.PlayerId == id);
            if (player == null || !User.IsTeamIdValidForUser(player.CurrentTeamId))
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<PlayerEditModel>(player);
            PopulateDropdownLists(model);

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, PlayerEditModel model)
        {
            var player = DbContext.Players.SingleOrDefault(p => p.PlayerId == id);
            if (player == null || !User.IsTeamIdValidForUser(player.CurrentTeamId))
            {
                return HttpNotFound();
            }

            Mapper.Map(model, player);
            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        #endregion

        #region Transfer/Merge

        [HttpPost]
        [Route("Transfer/{id:int}/{teamId:int}")]
        public ActionResult Transfer(int id, int teamId)
        {
            var player = DbContext.Players.SingleOrDefault(p => p.PlayerId == id);
            var team = DbContext.Teams.SingleOrDefault(t => t.TeamId == teamId);
            if (player == null || team == null || !(User.IsTeamIdValidForUser(player.CurrentTeamId) && User.IsTeamIdValidForUser(teamId)))
            {
                return HttpNotFound();
            }

            player.CurrentTeamId = teamId;
            DbContext.SaveChanges(User.Identity.GetUserId());

            if (Request.Cookies["lastTeamId"] == null || Request.Cookies["lastTeamId"].Value != teamId.ToString())
            {
                var cookie = new HttpCookie("lastTeamId", teamId.ToString()) {Expires = DateTime.MaxValue};
                Response.Cookies.Add(cookie);
            }

            var data = SelectPlayerEditModels(DbContext.Players
                                                       .Where(p => p.PlayerId == id)
                                                       .ToList());

            return Json(data);
        }

        [HttpPost]
        [Route("Merge")]
        public ActionResult Merge(int[] ids)
        {
            var players = DbContext.Players.Where(p => ids.Contains(p.PlayerId)).OrderBy(p => p.PlayerId).ToList();

            var playerToSave = players.First();
            playerToSave.IsActive = players.Any(p => p.IsActive);

            foreach (var player in players.Skip(1))
            {
                playerToSave.FirstName = player.FirstName;
                playerToSave.LastName = player.LastName;
                playerToSave.CurrentTeamId = player.CurrentTeamId;

                foreach (var statLine in player.StatLines)
                {
                    statLine.Player = playerToSave;
                }

                DbContext.Players.Remove(player);
            }

            DbContext.SaveChanges(User.Identity.GetUserId());

            return HttpOk();
        }

        #endregion

        #region Helpers

        private void PopulateDropdownLists(PlayerEditModel model)
        {
            var teams = DbContext.TeamYears
                                 .Where(ty => ty.Year == Consts.CurrentYear && !Consts.InvalidPlayerTeamIds.Contains(ty.TeamId))
                                 .OrderBy(ty => ty.DivisionYear.IsInLeague)
                                 .ThenBy(ty => ty.FullName)
                                 .ToList()
                                 .FilterTeamsForUser(User, ty => ty.TeamId)
                                 .Select(ty => new SelectListItem {Value = ty.TeamId.ToString(), Text = ty.FullName})
                                 .ToList();
            model.Team.ItemSelectList = new SelectList(teams, "Value", "Text", model.Team.TeamId);
        }

        public IEnumerable<PlayerEditModel> SelectPlayerEditModels(IEnumerable<Player> playerEntities)
        {
            var teamIds = DbContext.Teams
                                   .Select(t => t.TeamId)
                                   .FilterTeamsForUser(User, id => id)
                                   .ToList();
            var teamId = teamIds.Count == 1
                             ? teamIds[0]
                             : 0;

            return playerEntities.Select(p =>
                                         {
                                             var model = Mapper.Map<PlayerEditModel>(p);
                                             model.UrlForEdit = Url.Action("Edit", new {id = model.PlayerId});
                                             model.Team.UrlForTransfer = Url.Action("Transfer",
                                                                                 new
                                                                                 {
                                                                                     id = model.PlayerId,
                                                                                     teamId = model.Team.TeamId == Consts.PlayerPoolTeamId
                                                                                                  ? teamId
                                                                                                  : Consts.PlayerPoolTeamId
                                                                                 });

                                             return model;
                                         });
        }

        #endregion
    }
}