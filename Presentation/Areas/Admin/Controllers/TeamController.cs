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
    [RoutePrefix("Team")]
    public class TeamController : AdminControllerBase
    {
        #region List

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("{year:year?}")]
        public ActionResult List(int year = Consts.CurrentYear)
        {
            return View(new TeamEditModel {Year = year});
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [HttpPost]
        [Route("Data/{year:year}")]
        public JsonResult Data(int year)
        {
            var data = DbContext.TeamYears
                                .Where(ty => ty.Year == year)
                                .ToList()
                                .Select(ty =>
                                        {
                                            var model = Mapper.Map<TeamEditModel>(ty);
                                            model.EditUrl = Url.Action("Edit", new {id = model.TeamId});

                                            return model;
                                        });

            return Json(data);
        }

        #endregion

        #region Create/Edit

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("Create")]
        public ActionResult Create()
        {
            return View("Edit", new TeamEditModel());
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(TeamEditModel model)
        {
            var team = Mapper.Map<Team>(model);

            DbContext.Teams.Add(team);
            //DbContext.SaveChanges(User.Identity.Name);

            return RedirectToAction("List");
        }

        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var team = DbContext.Teams.SingleOrDefault(c => c.TeamId == id);
            if (team == null || (!User.IsInRole(Roles.LeagueCommissioner) && !User.IsTeamIdValidForUser(id)))
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<TeamEditModel>(team);

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, TeamEditModel model)
        {
            var team = DbContext.Teams.SingleOrDefault(c => c.TeamId == id);
            if (team == null || (!User.IsInRole(Roles.LeagueCommissioner) && !User.IsTeamIdValidForUser(id)))
            {
                return HttpNotFound();
            }

            Mapper.Map(model, team);
            //DbContext.SaveChanges(User.Identity.Name);

            return RedirectToAction("List");
        }

        #endregion
    }
}