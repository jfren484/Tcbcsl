using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;
using System.Data.Entity.Validation;

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
                                            model.EditUrl = Url.Action("Edit", new {id = model.TeamYearId});

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
            var teamYear = DbContext.TeamYears.SingleOrDefault(ty => ty.TeamYearId == id);
            if (teamYear == null || (!User.IsInRole(Roles.LeagueCommissioner) && !User.IsTeamIdValidForUser(teamYear.TeamId)))
            {
                return HttpNotFound();
            }   

            var model = Mapper.Map<TeamEditModel>(teamYear);

            var divisions = DbContext.DivisionYears
                                     .Where(dy => dy.Year == Consts.CurrentYear)
                                     .OrderBy(dy => dy.Sort)
                                     .Select(dy => new SelectListItem { Value = dy.DivisionId.ToString(), Text = dy.Name })
                                     .ToList();
            model.Division.ItemSelectList = new SelectList(divisions, "Value", "Text", model.Division.DivisionYearId);

            var churches = DbContext.Churches
                                    .OrderBy(c => c.FullName)
                                    .Select(c => new SelectListItem { Value = c.ChurchId.ToString(), Text = c.FullName })
                                    .ToList();
            model.Church.ItemSelectList = new SelectList(churches, "Value", "Text", model.Church.ChurchId);

            var coaches = DbContext.Coaches
                                   .OrderBy(c => c.LastName)
                                   .ThenBy(c => c.FirstName)
                                   .ToList()
                                   .Select(c => new SelectListItem { Value = c.CoachId.ToString(), Text = c.FullName })
                                   .ToList();
            model.HeadCoach.ItemSelectList = new SelectList(coaches, "Value", "Text", model.HeadCoach.CoachId);

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, TeamEditModel model)
        {
            var teamYear = DbContext.TeamYears.SingleOrDefault(ty => ty.TeamYearId == id);
            if (teamYear == null || (!User.IsInRole(Roles.LeagueCommissioner) && !User.IsTeamIdValidForUser(teamYear.TeamId)))
            {
                return HttpNotFound();
            }

            Mapper.Map(model, teamYear);
            var churchName = DbContext.Churches.Single(ch => ch.ChurchId == teamYear.ChurchId).DisplayName;
            teamYear.FullName = $"{churchName} {teamYear.TeamName}".Trim();

            try
            {
                DbContext.SaveChanges(User.Identity.Name);
            }
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors.ToList();
                throw;
            }

            return RedirectToAction("List");
        }

        #endregion
    }
}