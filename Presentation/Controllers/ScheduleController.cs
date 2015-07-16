using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Models;
using Tcbcsl.Presentation.Services;

namespace Tcbcsl.Presentation.Controllers
{
    public class ScheduleController : ControllerBase
    {
        #region Schedule

        [Route("Schedule/{date:datetime?}")]
        public ActionResult Schedule(DateTime? date)
        {
            var scheduleDate = date ?? GetClosestGameDate();

            var gamesOnDate = DbContext.Games
                                       .Where(g => SqlFunctions.DateDiff("day", g.GameDate, scheduleDate) == 0)
                                       .ToList();

            if (!gamesOnDate.Any())
            {
                return new HttpNotFoundResult();
            }

            var bucketedGames = new Dictionary<GameBucket, List<Game>>();
            foreach (var game in gamesOnDate)
            {
                var bucket = GetGameBucket(game);

                if (!bucketedGames.ContainsKey(bucket))
                {
                    bucketedGames.Add(bucket, new List<Game>());
                }

                bucketedGames[bucket].Add(game);
            }

            var model = new ScheduleModel
                        {
                            Date = scheduleDate,
                            ConferenceModels = bucketedGames.OrderBy(kvp => kvp.Key.Sort)
                                                            .Select(kvp => new ScheduleConferenceModel
                                                                           {
                                                                               Label = kvp.Key.Label,
                                                                               Games = kvp.Value.Select(ScheduleService.GameModelFromGame).ToList()
                                                                           })
                                                            .ToList()
                        };

            return View(model);
        }

        #region Helpers

        private static GameBucket GetGameBucket(Game game)
        {
            if (game.GameTypeId == GameType.GamePlaceholder)
            {
                return new GameBucket("Post-Season", 0);
            }

            if (Consts.TournamentDates.Contains(game.GameDate.Date))
            {
                return new GameBucket(game.GameDate.ToString("t"), (int)game.GameDate.TimeOfDay.TotalMinutes);
            }

            var conferences = game.GameParticipants
                                  .Select(gp => gp.TeamYear.DivisionYear.ConferenceYear)
                                  .DistinctBy(cy => cy.ConferenceYearId)
                                  .ToList();

            if (game.GameTypeId == GameType.Exhibition || conferences.Any(cy => !cy.IsInLeague))
            {
                return new GameBucket("Exhibition", 99);
            }

            if (conferences.Count > 1 && conferences[0].ConferenceYearId != conferences[1].ConferenceYearId)
            {
                return new GameBucket("Inter-Conference", 90);
            }

            return new GameBucket(conferences[0]);
        }

        private DateTime GetClosestGameDate()
        {
            var dateQuery = from g in DbContext.Games
                            orderby Math.Abs(SqlFunctions.DateDiff("day", g.GameDate, DateTime.Now).Value)
                            select g.GameDate;

            return dateQuery.First().Date;
        }

        private struct GameBucket : IEquatable<GameBucket>
        {
            public string Label { get; }
            public int Sort { get; }

            public GameBucket(string label, int sort)
            {
                Label = label;
                Sort = sort;
            }

            public GameBucket(ConferenceYear conference)
            {
                Label = conference.Name;
                Sort = conference.Sort;
            }

            public bool Equals(GameBucket other)
            {
                return other.Label == Label;
            }
        }

        #endregion

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

            var gameDatesInYear = new List<DateTime> { new DateTime(year, 5, 11), new DateTime(year, 8, 15) };
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

        #endregion
    }
}