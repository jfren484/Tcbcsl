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
                                            model.UrlForEdit = Url.Action("Edit", new {id = model.TeamYearId});

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
            var model = new TeamEditModel { Year = Consts.CurrentYear };

            PopulateDropdownLists(model);

            return View("Edit", model);
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(TeamEditModel model)
        {
            var teamYear = Mapper.Map<TeamYear>(model);
            teamYear.Year = Consts.CurrentYear;
            PopulateFullname(teamYear);

            DbContext.TeamYears.Add(teamYear);
            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var teamYear = DbContext.TeamYears.SingleOrDefault(ty => ty.TeamYearId == id);
            if (teamYear == null || !User.IsTeamIdValidForUser(teamYear.TeamId))
            {
                return HttpNotFound();
            }   

            var model = Mapper.Map<TeamEditModel>(teamYear);
            PopulateDropdownLists(model);

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, TeamEditModel model)
        {
            var teamYear = DbContext.TeamYears.SingleOrDefault(ty => ty.TeamYearId == id);
            if (teamYear == null || !User.IsTeamIdValidForUser(teamYear.TeamId))
            {
                return HttpNotFound();
            }

            Mapper.Map(model, teamYear);
            PopulateFullname(teamYear);

            DbContext.SaveChanges(User.Identity.GetUserId());

            if (id == Consts.PlayerPoolTeamId)
            {
                Consts.PlayerPoolTeamName = teamYear.FullName;
            }

            return Redirect(model.UrlForReturn);
        }

        #endregion

        #region Manage

        [Route("Manage/{id:int}")]
        public ActionResult Manage(int id)
        {
            var model = DbContext.TeamYears
                                 .Where(ty => ty.TeamId == id)
                                 .OrderByDescending(ty => ty.Year)
                                 .Take(1)
                                 .ToList()
                                 .Select(Mapper.Map<TeamManageModel>)
                                 .First();

            model.Church.Address = model.Church.Address ?? new AddressEditModel();
            model.Church.Address.State = model.Church.Address.State ?? new StateEditModel();
            model.Church.Address.State.ItemSelectList = new SelectList(new string[0]);

            model.HeadCoach.Address = model.HeadCoach.Address ?? new AddressEditModel();
            model.HeadCoach.Address.State = model.HeadCoach.Address.State ?? new StateEditModel();
            model.HeadCoach.Address.State.ItemSelectList = new SelectList(new string[0]);

            return View(model);
        }

        #endregion

        #region Helpers

        private void PopulateDropdownLists(TeamEditModel model)
        {
            var divisions = DbContext.DivisionYears
                                     .Where(dy => dy.Year == Consts.CurrentYear)
                                     .OrderBy(dy => dy.Sort)
                                     .Select(dy => new SelectListItem { Value = dy.DivisionYearId.ToString(), Text = dy.Name })
                                     .ToList();
            divisions.Insert(0, new SelectListItem());
            model.Division.ItemSelectList = new SelectList(divisions, "Value", "Text", model.Division.DivisionYearId);

            var churches = DbContext.Churches
                                    .OrderBy(c => c.FullName)
                                    .Select(c => new SelectListItem { Value = c.ChurchId.ToString(), Text = c.FullName })
                                    .ToList();
            churches.Insert(0, new SelectListItem());
            model.Church.ItemSelectList = new SelectList(churches, "Value", "Text", model.Church.ChurchId);

            var coaches = DbContext.Coaches
                                   .OrderBy(c => c.LastName)
                                   .ThenBy(c => c.FirstName)
                                   .ToList()
                                   .Select(c => new SelectListItem { Value = c.CoachId.ToString(), Text = c.FullName })
                                   .ToList();
            coaches.Insert(0, new SelectListItem());
            model.HeadCoach.ItemSelectList = new SelectList(coaches, "Value", "Text", model.HeadCoach.CoachId);

            var clinchItems = Consts.ClinchDescriptions
                                    .Select(kvp => new SelectListItem {Value = kvp.Key, Text = kvp.ClinchDescriptionFormatted()})
                                    .ToList();
            clinchItems.Insert(0, new SelectListItem {Text = "(none)"});
            model.Clinch.ItemSelectList = new SelectList(clinchItems, "Value", "Text", model.Clinch.ClinchChar);
        }

        private void PopulateFullname(TeamYear teamYear)
        {
            var churchName = DbContext.Churches.Single(ch => ch.ChurchId == teamYear.ChurchId).DisplayName;
            teamYear.FullName = $"{churchName} {teamYear.TeamName}".Trim();
        }

        #endregion
    }
}