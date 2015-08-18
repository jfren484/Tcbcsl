using System.Web.Mvc;
using Tcbcsl.Presentation.Services;

namespace Tcbcsl.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContentService _contentService;

        public HomeController(ContentService contentService)
        {
            _contentService = contentService;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View(_contentService.GetCurrentNews());
        }
    }
}