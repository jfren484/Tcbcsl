using System.Web.Mvc;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    public class StatisticsController : ControllerBase
    {
        [Route("Statistics")]
        public ActionResult Statistics(StatisticsFilterModel filterModel)
        {
            return View(filterModel);
        }
    }
}