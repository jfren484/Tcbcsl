using System;
using System.Web.Mvc;
using Tcbcsl.Presentation.Services;

namespace Tcbcsl.Presentation.Controllers
{
    [RoutePrefix("Schedule")]
    public class ScheduleController : ControllerBase
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
                return HttpNotFound($"No games found for {date?.ToString(Consts.DateFormatDisplay)}.");
            }

            return View(model);
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