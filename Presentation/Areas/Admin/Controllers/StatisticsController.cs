using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
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
        #region Game

        [Route("{id:int}")]
        public ActionResult Game(int id)
        {
            var gameParticipant = DbContext.GameParticipants.SingleOrDefault(gp => gp.GameParticipantId == id);
            if (gameParticipant == null || !User.IsTeamIdValidForUser(gameParticipant.TeamYear.TeamId))
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<StatisticsEditModel>(gameParticipant);
            var newGame = !model.StatLines.Any();
            if (newGame)
            {
                model.StatLines.AddRange(Enumerable.Repeat(new StatisticsEditStatLineModel {Player = new StatisticsEditPlayerModel()}, 10));
            }
            PopulateDropdownLists(model, gameParticipant.TeamYear.TeamId, newGame);
            model.UrlForNewRow = Url.Action("Row", new {id = gameParticipant.TeamYear.TeamId});
            model.UrlForReturn = Url.Action("List", "GameResults", new
                                                                   {
                                                                       id = gameParticipant.TeamYear.TeamId,
                                                                       year = gameParticipant.TeamYear.Year.AsRouteParameter()
                                                                   });

            return View(model);
        }

        [HttpPost]
        [Route("{id:int}")]
        public ActionResult Game(int id, StatisticsEditModel model)
        {
            var gameParticipant = DbContext.GameParticipants.SingleOrDefault(gp => gp.GameParticipantId == id);
            if (gameParticipant == null || !User.IsTeamIdValidForUser(gameParticipant.TeamYear.TeamId))
            {
                return HttpNotFound();
            }

            var changes = ChangeTracker.GetChangeSets(gameParticipant.StatLines, model.StatLines, e => e.StatLineId, m => m.StatLineId);

            foreach (var pair in changes.CommonItems)
            {
                Mapper.Map(pair.RightItem, pair.LeftItem);
            }

            foreach (var statLineModel in changes.RightOnly)
            {
                gameParticipant.StatLines.Add(Mapper.Map<StatLine>(statLineModel));
            }

            DbContext.StatLines.RemoveRange(changes.LeftOnly);

            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        #endregion

        #region Partials

        [HttpPost]
        [Route("NewRow/{id:int}")]
        public ActionResult Row(int id)
        {
            if (!User.IsTeamIdValidForUser(id))
            {
                return HttpNotFound();
            }

            var model = new StatisticsEditModel
            {
                StatLines = new List<StatisticsEditStatLineModel>
                {
                    new StatisticsEditStatLineModel
                    {
                        Player = new StatisticsEditPlayerModel()
                    }
                }
            };
            PopulateDropdownLists(model, id, true);

            return PartialView(model);
        }

        #endregion

        #region Helpers

        private void PopulateDropdownLists(StatisticsEditModel model, int teamId, bool includeEmptyOption)
        {
            var playerList = DbContext.Players
                            .Where(p => p.CurrentTeamId == teamId && p.IsActive)
                            .OrderBy(p => p.LastName)
                            .ThenBy(p => p.FirstName)
                            .ToList()
                            .Select(p => new SelectListItem { Value = p.PlayerId.ToString(), Text = p.FullName })
                            .ToList();

            if (includeEmptyOption)
            {
                playerList.Insert(0, new SelectListItem());
            }

            model.StatLines.ForEach(sl =>
                                    {
                                        sl.Player.ItemSelectList = new SelectList(playerList, "Value", "Text", sl.Player.PlayerId);
                                    });
        }

        #endregion
    }
}