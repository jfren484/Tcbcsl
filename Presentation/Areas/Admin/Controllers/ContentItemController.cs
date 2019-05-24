using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Tcbcsl.Data;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
    [Route("Content")]
    public class ContentItemController : AdminControllerBase
    {
        public ContentItemController(TcbcslDbContext dbContext) : base(dbContext) { }

        #region List

        [Route("")]
        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Data()
        {
            var data = DbContext.PageContents
                                .OrderBy(pc => pc.PageContentId)
                                .ToList()
                                .Select(pc =>
                                        {
                                            var model = Mapper.Map<PageContentEditModel>(pc);
                                            model.UrlForEdit = Url.Action("Edit", new {id = model.PageContentId});

                                            return model;
                                        });

            return Json(data);
        }

        #endregion

        #region Create/Edit

        public ActionResult Create()
        {
            return View("Edit", new PageContentEditModel());
        }

        [HttpPost]
        public ActionResult Create(PageContentEditModel model)
        {
            var contentItem = Mapper.Map<PageContent>(model);
            DbContext.PageContents.Add(contentItem);
            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var contentItem = DbContext.PageContents.SingleOrDefault(pc => pc.PageContentId == id);
            if (contentItem == null)
            {
                return NotFound();
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
                return NotFound();
            }

            Mapper.Map(model, contentItem);
            DbContext.SaveChanges(User.Identity.GetUserId());

            return Redirect(model.UrlForReturn);
        }

        #endregion
    }
}