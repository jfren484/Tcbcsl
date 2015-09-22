using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Presentation.Areas.Admin.Models;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [Authorize(Roles = "League Commissioner")]
    [RouteArea("Admin")]
    [RoutePrefix("User")]
    public class UserController : AdminControllerBase
    {
        #region User List

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
                                    var userRoleIds = u.Roles.Select(r => r.RoleId).ToList();
                                    var model = Mapper.Map<UserEditModel>(u);

                                    model.Roles.SelectedRoleNames = Mapper.Map<string>(DbContext.Roles.Where(r => userRoleIds.Contains(r.Id)));
                                    model.EditUrl = Url.Action("Edit", new { id = model.Id });

                                    return model;
                                });

            return Json(data);
        }

        #endregion

        #region Create/Edit

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
            DbContext.SaveChanges();

            return RedirectToAction("List");
        }

        #endregion
    }
}