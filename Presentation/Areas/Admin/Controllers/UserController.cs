using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CsQuery.ExtensionMethods.Internal;
using Microsoft.AspNet.Identity.Owin;
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
                                            model.UrlForEdit = Url.Action("Edit", new {id = model.Id});

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
            var user = Mapper.Map<UserEditModel>(DbContext.Users.SingleOrDefault(u => u.Id == id));
            if (user == null)
            {
                return HttpNotFound();
            }

            var allTeams = DbContext.Teams
                                    .Where(t => t.TeamYears.Any())
                                    .OrderByDescending(t => t.TeamYears.OrderByDescending(ty => ty.Year).FirstOrDefault().Year)
                                    .ThenBy(t => t.TeamYears.OrderByDescending(ty => ty.Year).FirstOrDefault().FullName)
                                    .ToList();

            user.Roles.SelectedRoleNames = Mapper.Map<string>(DbContext.Roles.Where(r => user.Roles.RoleIds.Contains(r.Id)));
            user.Roles.AllRoles = Mapper.Map<List<SelectListItem>>(DbContext.Roles);
            user.AssignedTeams.AllTeams = Mapper.Map<List<SelectListItem>>(allTeams);

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
            var teamChanges = ChangeTracker.GetChangeSets(user.AssignedTeams, model.AssignedTeams?.TeamIds ?? new List<int>(), t => t.TeamId, i => i);
            foreach (var teamToRemove in teamChanges.LeftOnly)
            {
                user.AssignedTeams.Remove(teamToRemove);
            }
            if (teamChanges.RightOnly.Any())
            {
                user.AssignedTeams.AddRange(DbContext.Teams.Where(t => teamChanges.RightOnly.Contains(t.TeamId)));
            }

            // Update roles
            var roleChanges = ChangeTracker.GetChangeSets(user.Roles, model.Roles.RoleIds ?? new List<string>(), r => r.RoleId, s => s);
            foreach (var roleToRemove in roleChanges.LeftOnly)
            {
                user.Roles.Remove(roleToRemove);
            }
            if (roleChanges.RightOnly.Any())
            {
                user.Roles.AddRange(DbContext.Roles
                                             .ToList()
                                             .Where(r => roleChanges.RightOnly.Contains(r.Id))
                                             .Select(r => new IdentityUserRole
                                                          {
                                                              UserId = user.Id,
                                                              RoleId = r.Id
                                                          }));
            }

            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        #endregion

        #region Reset Password

        [Route("ResetPassword/{id}")]
        [HttpPost]
        public async Task<ActionResult> ResetUserPassword(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var resetToken = await userManager.GeneratePasswordResetTokenAsync(id);
            var passwordChangeResult = await userManager.ResetPasswordAsync(id, resetToken, "Password#1");

            return Json(passwordChangeResult);
        }

        #endregion
    }
}