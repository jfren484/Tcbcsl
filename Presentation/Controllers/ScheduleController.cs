using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    public class ScheduleController : ControllerBase
    {
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
                                                                               Games = kvp.Value.Select(GameModelFromGame).ToList()
                                                                           })
                                                            .ToList()
                        };

            return View(model);
        }

        private ScheduleGameModel GameModelFromGame(Game game)
        {
            return new ScheduleGameModel
                   {
                       GameId = game.GameId,
                       GameDate = game.GameDate,
                       DisplayScores = game.GameStatus.DisplayScores,
                       Outcome = game.GameStatus.Description,
                       HomeTeam = game.GameParticipants.Where(gp => gp.IsHost).Select(GameTeamModelFromParticipant).Single(),
                       RoadTeam = game.GameParticipants.Where(gp => !gp.IsHost).Select(GameTeamModelFromParticipant).Single()
                   };
        }

        private ScheduleGameTeamModel GameTeamModelFromParticipant(GameParticipant gp)
        {
            return new ScheduleGameTeamModel
                   {
                       TeamId = gp.TeamYear.TeamId,
                       TeamName = gp.TeamYear.Church.DisplayName + (string.IsNullOrEmpty(gp.TeamYear.TeamName)
                                                                        ? null
                                                                        : " " + gp.TeamYear.TeamName),
                       RecordInfo = "", // TODO: figure this out
                       RunsScored = gp.RunsScored,
                       Hits = gp.StatLines.Any() ? gp.StatLines.Sum(sl => sl.StatHits) : (int?)null
                   };
        }

        private GameBucket GetGameBucket(Game game)
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
    }
}