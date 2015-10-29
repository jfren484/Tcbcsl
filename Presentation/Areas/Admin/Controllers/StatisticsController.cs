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