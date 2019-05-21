using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner + ", " + Roles.TeamCoach)]
    public class TeamController : AdminControllerBase
    {
        #region List

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("{year:year?}")]
        public ActionResult List(int year = Consts.CurrentYear)
        {
            return View(new TeamEditModel {YearModel = new YearModel {Year = year}});
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [HttpPost]
        [Route("{year:year?}")]
        public ActionResult ListSave(int year, ICollection<TeamListEditModel> teams)
        {
            foreach (var team in teams)
            {
                var teamYear = DbContext.TeamYears.Single(ty => ty.TeamYearId == team.TeamYearId);
                Mapper.Map(team, teamYear);
            }

            DbContext.SaveChanges(User.Identity.GetUserId());

            return RedirectToAction("List", new { Year = year });
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

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("ListDropdownData/{year:year}")]
        public ActionResult ListDropdownData(int year)
        {
            var model = new TeamListCommonEditModel
            {
                Division = new TeamEditDivisionModel { ItemSelectList = new SelectList(GetDivisionSelectListItems(false, year), "Value", "Text") },
                Clinch = new TeamEditClinchModel { ItemSelectList = new SelectList(GetClinchSelectListItems(), "Value", "Text") }
            };

            return PartialView(model);
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [Route("Years/{year:year?}")]
        public ActionResult ListTeamYears(int year = Consts.CurrentYear)
        {
            return View(new TeamEditModel {YearModel = new YearModel {Year = year}});
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [HttpPost]
        [Route("Years/Data/{year:year}")]
        public JsonResult TeamYearData(int year)
        {
            var data = DbContext.TeamYears
                                .Where(ty => ty.Year == year)
                                .Select(ty => new
                                              {
                                                  TeamYear = ty,
                                                  ExistsInCurrentYear = ty.Team.TeamYears.Any(ty2 => ty2.Year == Consts.CurrentYear)
                                              })
                                .ToList()
                                .Select(ty =>
                                        {
                                            var model = Mapper.Map<TeamYearTransferModel>(ty.TeamYear);
                                            model.ExistsInCurrentYear = ty.ExistsInCurrentYear;

                                            return model;
                                        });

            return Json(data);
        }

        #endregion

        #region Create/Edit

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        public ActionResult Create()
        {
            var model = new TeamEditModel {YearModel = new YearModel()};

            PopulateDropdownLists(model);

            return View("Edit", model);
        }

        [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
        [HttpPost]
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
                return NotFound();
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
                return NotFound();
            }

            Mapper.Map(model, teamYear);
            Mapper.Map(model, teamYear.Team);
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

        #region Transfer

        public ActionResult Transfer(int[] teamYearIds)
        {
            var missingTeams = DbContext.TeamYears
                                        .Where(ty => teamYearIds.Contains(ty.TeamYearId) && !ty.Team.TeamYears.Any(ty2 => ty2.Year == Consts.CurrentYear))
                                        .ToList();

            if (!missingTeams.Any())
            {
                return RedirectToAction("ListTeamYears");
            }

            if (!DbContext.ConferenceYears.Any(cy => cy.Year == Consts.CurrentYear))
            {
                var prevConferenceYears = DbContext.ConferenceYears
                                                   .Where(cy => cy.Year == Consts.CurrentYear - 1)
                                                   .ToList();

                var newConferenceYears = prevConferenceYears
                    .Select(cy => new ConferenceYear
                                  {
                                      ConferenceId = cy.ConferenceId,
                                      Year = Consts.CurrentYear,
                                      Name = cy.Name,
                                      IsInLeague = cy.IsInLeague,
                                      Sort = cy.Sort,
                                      DivisionYears = cy.DivisionYears
                                                        .Select(dy => new DivisionYear
                                                                      {
                                                                          DivisionId = dy.DivisionId,
                                                                          Year = Consts.CurrentYear,
                                                                          Name = dy.Name,
                                                                          IsInLeague = dy.IsInLeague,
                                                                          Sort = dy.Sort
                                                                      })
                                                        .ToList()
                                  })
                    .ToList();

                DbContext.ConferenceYears.AddRange(newConferenceYears);
                DbContext.SaveChanges(User.Identity.GetUserId());
            }

            var divYearIds = DbContext.DivisionYears
                                      .Where(dy => dy.Year == Consts.CurrentYear)
                                      .ToDictionary(dy => dy.DivisionId, dy => dy.DivisionYearId);

            DbContext.TeamYears
                     .AddRange(missingTeams.Select(ty =>
                                                   {
                                                       var divisionYearId = divYearIds.ContainsKey(ty.DivisionYear.DivisionId)
                                                                                ? divYearIds[ty.DivisionYear.DivisionId]
                                                                                : divYearIds[Consts.NonLeagueDivisionId];
                                                       return new TeamYear
                                                              {
                                                                  TeamId = ty.TeamId,
                                                                  Year = Consts.CurrentYear,
                                                                  TeamName = ty.TeamName,
                                                                  FullName = ty.FullName,
                                                                  DivisionYearId = divisionYearId,
                                                                  ChurchId = ty.ChurchId,
                                                                  HeadCoachId = ty.HeadCoachId,
                                                                  KeepsStats = ty.KeepsStats,
                                                                  HasPaid = false
                                                              };
                                                   }));
            DbContext.SaveChanges(User.Identity.GetUserId());

            return RedirectToAction("ListTeamYears");
        }

        #endregion

        #region Helpers

        private void PopulateDropdownLists(TeamEditModel model)
        {
            model.Division.ItemSelectList = new SelectList(GetDivisionSelectListItems(), "Value", "Text", model.Division.DivisionYearId);

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

            model.Clinch.ItemSelectList = new SelectList(GetClinchSelectListItems(), "Value", "Text", model.Clinch.ClinchChar);
        }

        private List<SelectListItem> GetClinchSelectListItems()
        {
            var clinchItems = Consts.ClinchDescriptions
                                    .Select(kvp => new SelectListItem { Value = kvp.Key, Text = kvp.ClinchDescriptionFormatted() })
                                    .ToList();
            clinchItems.Insert(0, new SelectListItem { Text = "(none)" });

            return clinchItems;
        }

        private List<SelectListItem> GetDivisionSelectListItems(bool appendEmptyItem = true, int year = Consts.CurrentYear)
        {
            var divisions = DbContext.DivisionYears
                                     .Where(dy => dy.Year == year)
                                     .OrderBy(dy => dy.Sort)
                                     .Select(dy => new SelectListItem { Value = dy.DivisionYearId.ToString(), Text = dy.Name })
                                     .ToList();

            if (appendEmptyItem)
            {
                divisions.Insert(0, new SelectListItem());
            }

            return divisions;
        }

        private void PopulateFullname(TeamYear teamYear)
        {
            var churchName = DbContext.Churches.Single(ch => ch.ChurchId == teamYear.ChurchId).DisplayName;
            teamYear.FullName = $"{churchName} {teamYear.TeamName}".Trim();
        }

        #endregion
    }
}