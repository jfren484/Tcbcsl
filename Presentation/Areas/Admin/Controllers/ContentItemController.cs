using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Content")]
    public class ContentItemController : Controller
    {
        [Route("")]
        public ActionResult List()
        {
            return View();
        }
    }
}