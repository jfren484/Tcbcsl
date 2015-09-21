using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Data.Identity;
using Tcbcsl.Presentation.Areas.Admin.Models;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
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
                                    var userRoleIds = u.Roles.Select(r => r.RoleId);
                                    var model = Mapper.Map<UserEditModel>(u);

                                    model.EditUrl = Url.Action("Edit", new { id = model.Id });
                                    model.RoleList = string.Join(", ", DbContext.Roles.Where(r => userRoleIds.Contains(r.Id)).Select(r => r.Name));

                                    return model;
                                });

            return Json(data);
        }

        #endregion

        #region Create/Edit

        [Route("Edit/{id}")]
        public ActionResult Edit(string id)
        {
            var user = DbContext.Users
                                .ToList()
                                .Select(u =>
                                {
                                    var model = Mapper.Map<UserEditModel>(u);

                                    //model.RoleIds = DbContext.Roles.Where(r => userRoleIds.Contains(r.Id)).Select(r => r.Id).ToList();

                                    return model;
                                }).SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public ActionResult Edit(string id, NewsEditModel model)
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