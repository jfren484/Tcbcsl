using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Presentation.Models;
using Tcbcsl.Presentation.Services;

namespace Tcbcsl.Presentation.Controllers
{
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

        [Route("Schedule/{date:datetime?}")]
        public ActionResult Schedule(DateTime? date)
        {
            var model = _scheduleService.GetScheduleModelForDate(date);

            if (model == null)
            {
                return new HttpNotFoundResult();
            }

            return View(model);
        }

        #endregion

        #region YearCalendar

        [Route("Schedule/YearCalendar/{year:year}/{activeDate:datetime}")]
        public PartialViewResult YearCalendar(int year, DateTime activeDate)
        {
            var model = new YearCalendarModel
            {
                Year = year,
                ActiveDate = activeDate,
                Months = new List<YearCalendarMonthModel>()
            };

            var gameDatesInYear = GetGameDatesInYear(year);
            var firstMonth = gameDatesInYear.Min().Month;
            var lastMonth = gameDatesInYear.Max().Month;

            for (var month = firstMonth; month <= lastMonth; ++month)
            {
                var firstDay = new DateTime(year, month, 1);
                var daysInMonth = DateTime.DaysInMonth(year, month);

                var dayList = Enumerable.Repeat(0, (int)firstDay.DayOfWeek)
                                        .Concat(Enumerable.Range(1, daysInMonth))
                                        .Concat(Enumerable.Repeat(0, 6 - (int)new DateTime(year, month, daysInMonth).DayOfWeek));

                model.Months.Add(new YearCalendarMonthModel
                                 {
                                     Month = month,
                                     MonthName = firstDay.ToString("MMMM"),
                                     Weeks = dayList.Batch(7)
                                                    .Select(week => week.Select(day => new YearCalendarDayModel
                                                                                       {
                                                                                           Day = day,
                                                                                           Date = day == 0 ? DateTime.MinValue : new DateTime(year, month, day),
                                                                                           HasGames = day != 0 && gameDatesInYear.Contains(new DateTime(year, month, day))
                                                                                       })
                                                                        .ToList())
                                                    .ToList()
                                 });
            }

            return PartialView(model);
        }

        private List<DateTime> GetGameDatesInYear(int year)
        {
            var dates = (from g in DbContext.Games
                         where g.GameDate.Year == year
                         select g.GameDate)
                .DistinctBy(g => g.Date)
                .Select(d => d.Date)
                .ToList();

            return dates;
        } 

        #endregion
    }
}