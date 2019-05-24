using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcbcsl.Data;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
    public class UserController : AdminControllerBase
    {
        public UserController(TcbcslDbContext dbContext) : base(dbContext) { }

        #region List

        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Data()
        {
            var data = DbContext.Users
                                .ToList()
                                .Select(u =>
                                        {
                                            var model = Mapper.Map<UserEditModel>(u);
                                            model.UrlForEdit = Url.Action("Edit", new {id = model.Id});

                                            var usersRoles = (from ur in DbContext.UserRoles
                                                              join r in DbContext.Roles
                                                              on ur.RoleId equals r.Id
                                                              where ur.UserId == model.Id
                                                              select r).ToList();
                                            model.Roles.SelectedRoleNames = Mapper.Map<string>(usersRoles);

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
                return NotFound();
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
                return NotFound();
            }

            Mapper.Map(model, user);

            // Update assigned teams
            var teamChanges = ChangeTracker.GetChangeSets(user.TcbcslUserTeams, model.AssignedTeams?.TeamIds ?? new List<int>(), t => t.TeamId, i => i);
            foreach (var tcbcslUserTeamToRemove in teamChanges.LeftOnly)
            {
                user.TcbcslUserTeams.Remove(tcbcslUserTeamToRemove);
            }
            foreach (var teamId in teamChanges.RightOnly)
            {
                user.TcbcslUserTeams.Add(new TcbcslUserTeam { UserId = id, TeamId = teamId });
            }

            // Update roles
            var userRoles = DbContext.UserRoles.Where(ur => ur.UserId == user.Id).ToList();
            var modelRoleIds = model.Roles.RoleIds ?? new List<string>();
            var roleChanges = ChangeTracker.GetChangeSets(userRoles, modelRoleIds, r => r.RoleId, s => s);

            if (roleChanges.LeftOnly.Any())
            {
                DbContext.UserRoles.RemoveRange(roleChanges.LeftOnly);
            }

            if (roleChanges.RightOnly.Any())
            {
                var newUserRoles = roleChanges.RightOnly.Select(roleId => new IdentityUserRole<string> { UserId = user.Id, RoleId = roleId });
                DbContext.UserRoles.AddRange(newUserRoles);
            }

            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        #endregion

        //#region Reset Password

        //[Route("ResetPassword/{id}")]
        //[HttpPost]
        //public async Task<ActionResult> ResetUserPassword(string id)
        //{
        //    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    var resetToken = await userManager.GeneratePasswordResetTokenAsync(id);
        //    var passwordChangeResult = await userManager.ResetPasswordAsync(id, resetToken, "Password#1");

        //    return Json(passwordChangeResult);
        //}

        //#endregion
    }
}