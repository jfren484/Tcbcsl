using Microsoft.AspNetCore.Mvc;
using System;
using Tcbcsl.Data;
using Tcbcsl.Presentation.Services;

namespace Tcbcsl.Presentation.Controllers
{
    public class ScheduleController : TcbcslControllerBase
    {
        #region Constructor and Private Fields

        private readonly ScheduleService _scheduleService;

        public ScheduleController(TcbcslDbContext dbContext) : base(dbContext)
        {
            // TODO: dependency injection
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
                return NotFound($"No games found for {date?.ToString(Consts.DateFormatDisplay)}.");
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