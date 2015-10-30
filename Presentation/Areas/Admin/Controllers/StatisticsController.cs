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
            PopulateDropdownLists(model, gameParticipant.TeamYear.TeamId);
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

            //Mapper.Map(model, gameParticipant);

            //DbContext.SaveChanges(User.Identity.Name);

            return Redirect(model.UrlForReturn);
        }

        #endregion

        #region Helpers

        private void PopulateDropdownLists(StatisticsEditModel model, int teamId)
        {
            var players = DbContext.Players
                                   .Where(p => p.CurrentTeamId == teamId && p.IsActive)
                                   .OrderBy(p => p.NameLast)
                                   .ThenBy(p => p.NameFirst)
                                   .ToList()
                                   .Select(p => new SelectListItem { Value = p.PlayerId.ToString(), Text = p.FullName })
                                   .ToList();

            model.StatLines.ForEach(sl =>
                                    {
                                        sl.Player.ItemSelectList = new SelectList(players, "Value", "Text", sl.Player.PlayerId);
                                    });
        }

        #endregion
    }
}