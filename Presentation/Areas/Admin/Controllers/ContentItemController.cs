using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Presentation.Areas.Admin.Models;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Content")]
    public class ContentItemController : Presentation.Controllers.ControllerBase
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
            var data = Mapper.Map<List<PageContentEditModel>>(DbContext.PageContents.OrderBy(pc => pc.PageContentId));

            return Json(data);
        }

        #endregion

        #region Create/Edit

        [Route("Create")]
        public ActionResult Create()
        {
            var model = new PageContentEditModel {IsCreate = true};

            return View("Edit", model);
        }

        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var contentItem = DbContext.PageContents.SingleOrDefault(pc => pc.PageContentId == id);
            if (contentItem == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<PageContentEditModel>(contentItem);

            return View(model);
        }

        [Route("Edit")]
        public ActionResult Edit(PageContentEditModel model)
        {
            return View(model);
        }

        #endregion
    }
}