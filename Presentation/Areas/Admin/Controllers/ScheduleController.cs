using System;
using System.Web.Mvc;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;
using Tcbcsl.Presentation.Models;
using Tcbcsl.Presentation.Services;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner)]
    [RouteArea("Admin")]
    [RoutePrefix("Schedule")]
    public class ScheduleController : AdminControllerBase
    {
        #region Constructor and Private Fields

        private readonly ScheduleService _scheduleService;

        public ScheduleController()
        {
            _scheduleService = new ScheduleService(DbContext);
        }

        #endregion

        #region Schedule

        [Route("{date:datetime?}")]
        public ActionResult Schedule(DateTime? date)
        {
            var model = _scheduleService.GetScheduleModelForDate(date);

            if (model == null)
            {
                return new HttpNotFoundResult();
            }

            return View(model);
        }

        [HttpPost]
        [Route("{date:datetime?}")]
        public ActionResult Schedule(DateTime? date, ScheduleEditModel model)
        {
            return RedirectToAction("Schedule", new { date = date });
        }

        #endregion

        #region YearCalendar

        [Route("YearCalendar/{year:year}/{activeDate:datetime}")]
        public PartialViewResult YearCalendar(int year, DateTime activeDate)
        {
            var model = _scheduleService.GetYearCalendarModel(year, activeDate);

            return PartialView(model);
        }

        #endregion
    }
}