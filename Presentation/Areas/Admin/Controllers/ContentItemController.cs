﻿using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Data.Entities;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Content")]
    public class ContentItemController : AdminControllerBase
    {
        #region List

        [Route("")]
        public ActionResult List()
        {
            return View();
        }

        //[HttpPost]
        [Route("Data")]
        public JsonResult Data()
        {
            var data = DbContext.PageContents
                                .OrderBy(pc => pc.PageContentId)
                                .ToList()
                                .Select(pc =>
                                        {
                                            var model = Mapper.Map<PageContentEditModel>(pc);
                                            model.EditUrl = Url.Action("Edit", new {id = model.PageContentId});

                                            return model;
                                        });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create/Edit

        [Route("Create")]
        public ActionResult Create()
        {
            return View("Edit", new PageContentEditModel());
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(PageContentEditModel model)
        {
            var contentItem = Mapper.Map<PageContent>(model);
            UpdateCreatedFields(contentItem);
            DbContext.PageContents.Add(contentItem);
            DbContext.SaveChanges();

            return RedirectToAction("List");
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

        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, PageContentEditModel model)
        {
            var contentItem = DbContext.PageContents.SingleOrDefault(pc => pc.PageContentId == id);
            if (contentItem == null)
            {
                return HttpNotFound();
            }

            Mapper.Map(model, contentItem);
            UpdateModifiedFields(contentItem);
            DbContext.SaveChanges();

            return RedirectToAction("List");
        }

        #endregion
    }
}