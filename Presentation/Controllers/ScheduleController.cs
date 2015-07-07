using System;
using System.Web.Mvc;

namespace Tcbcsl.Presentation.Controllers
{
    public class ScheduleController : Controller
    {
        [Route("Schedule/{date:datetime?}")]
        public ActionResult Schedule(DateTime? date)
        {
            var scheduleDate = date ?? DateTime.Today;
            return View((object)scheduleDate.ToLongDateString());
        }
    }
}