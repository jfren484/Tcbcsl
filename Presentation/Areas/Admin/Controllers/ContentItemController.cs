using System.Linq;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Content")]
    public class ContentItemController : Presentation.Controllers.ControllerBase
    {
        [Route("")]
        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        [Route("Data")]
        public JsonResult Data()
        {
            var data = DbContext.PageContents.OrderBy(pc => pc.PageContentId);

            return Json(data);
        }
    }
}