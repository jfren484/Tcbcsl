using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tcbcsl.Presentation.Models;
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

        public IActionResult Index()
        {
            return View(_contentService.GetCurrentNews());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
