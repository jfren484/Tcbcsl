using System;
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

        [Route("Unauthorized")]
        public ActionResult Unauthorized()
        {
            return View();
        }

        [Route("Throw")]
        public ActionResult Throw()
        {
            throw new Exception("Test Error Page");
        }
    }
}