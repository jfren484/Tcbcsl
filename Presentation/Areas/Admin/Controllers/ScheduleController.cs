using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;
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
            var now = DateTime.Now;
            var games = date == null
                            ? DbContext.Games.Where(g => !g.GameStatus.DisplayOutcome && g.GameDate < now && g.GameDate.Year == Consts.CurrentYear)
                            : DbContext.Games.Where(g => SqlFunctions.DateDiff("day", g.GameDate, date.Value) == 0);

            var bucketedGames = games.ToList()
                                     .ToLookup(g => ScheduleService.GetGameBucketForEdit(g, date != null));

            var model = new ScheduleEditModel
                        {
                            Label = date?.ToString(Consts.DateFormatDisplay) ?? "All Unrecorded Games",
                            Date = date ?? now,
                            Buckets = Mapper.Map<List<ScheduleBucketEditModel>>(bucketedGames.OrderBy(g => g.Key.Sort))
                        };

            return View(model);
        }

        [HttpPost]
        [Route("{date:datetime?}")]
        public ActionResult Schedule(DateTime? date, ScheduleEditModel model)
        {
            var modelsForUpdate = model.Buckets
                                       .SelectMany(b => b.Games.Where(g => g.HomeTeam.RunsScored != 0 || g.RoadTeam.RunsScored != 0))
                                       .ToList();

            var gameIds = modelsForUpdate.Select(g => g.GameId)
                                         .ToArray();

            var toUpdate = DbContext.Games
                                    .Where(g => gameIds.Contains(g.GameId))
                                    .ToList()
                                    .Join(modelsForUpdate, g => g.GameId, m => m.GameId, (g, m) => new {Game = g, Model = m});

            foreach (var row in toUpdate)
            {
                row.Game.GameStatusId = GameStatus.Final;
                row.Game.GameParticipants.Single(gp => !gp.IsHost).RunsScored = row.Model.RoadTeam.RunsScored;
                row.Game.GameParticipants.Single(gp => gp.IsHost).RunsScored = row.Model.HomeTeam.RunsScored;
            }
            DbContext.SaveChanges(User.Identity.Name);

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