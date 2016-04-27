using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CsQuery.ExtensionMethods.Internal;
using Microsoft.AspNet.Identity;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
    [RouteArea("Admin")]
    [RoutePrefix("User")]
    public class UserController : AdminControllerBase
    {
        #region List

        [Route("")]
        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        [Route("Data")]
        public JsonResult Data()
        {
            var data = DbContext.Users
                                .ToList()
                                .Select(u =>
                                {
                                    var model = Mapper.Map<UserEditModel>(u);
                                    model.UrlForEdit = Url.Action("Edit", new { id = model.Id });

                                    var userRoleIds = u.Roles.Select(r => r.RoleId).ToList();
                                    model.Roles.SelectedRoleNames = Mapper.Map<string>(DbContext.Roles.Where(r => userRoleIds.Contains(r.Id)));

                                    return model;
                                });

            return Json(data);
        }

        #endregion

        #region Edit

        [Route("Edit/{id}")]
        public ActionResult Edit(string id)
        {
            var user = Mapper.Map<List<UserEditModel>>(DbContext.Users)
                             .SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            user.Roles.SelectedRoleNames = Mapper.Map<string>(DbContext.Roles.Where(r => user.Roles.RoleIds.Contains(r.Id)));
            user.Roles.AllRoles = Mapper.Map<List<SelectListItem>>(DbContext.Roles);
            user.AssignedTeams.AllTeams = Mapper.Map<List<SelectListItem>>(DbContext.Teams.Where(t => t.TeamYears.Any()).OrderByDescending(t => t.TeamYears.OrderByDescending(ty => ty.Year).FirstOrDefault().Year).ThenBy(t => t.TeamYears.OrderByDescending(ty => ty.Year).FirstOrDefault().FullName));

            return View(user);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public ActionResult Edit(string id, UserEditModel model)
        {
            var user = DbContext.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            Mapper.Map(model, user);

            // Update assigned teams
            var changes = ChangeTracker.GetChangeSets(user.AssignedTeams, model.AssignedTeams?.TeamIds, t => t.TeamId, i => i);
            foreach (var teamToRemove in changes.LeftOnly)
            {
                user.AssignedTeams.Remove(teamToRemove);
            }
            if (changes.RightOnly.Any())
            {
                user.AssignedTeams.AddRange(DbContext.Teams.Where(t => changes.RightOnly.Contains(t.TeamId)));
            }

            DbContext.SaveChanges(User.Identity.GetUserId());

            return RedirectToAction("List");
        }

        #endregion
    }
}