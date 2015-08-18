using System.Web.Mvc;

namespace Tcbcsl.Presentation.Areas.Manage.Controllers
{
    [RouteArea("Manage")]
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