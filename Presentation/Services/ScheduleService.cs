using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using Tcbcsl.Data;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Services
{
    public class ScheduleService
    {
        #region Constructor and Private Fields

        private readonly TcbcslDbContext _dbContext;

        public ScheduleService(TcbcslDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        public static string FormatRecord(int wins, int losses, int ties)
        {
            return $"{wins}-{losses}{ties:'-'#;;''}";
        }

        private ScheduleGameModel GameModelFromGame(Game game)
        {
            var teamRows = game.GameParticipants
                               .OrderBy(gp => gp.IsHost)
                               .Select(GameTeamRowModelFromParticipant)
                               .ToList();

            if (!teamRows[0].Runs.Equals(teamRows[1].Runs))
            {
                teamRows.OrderByDescending(tr => tr.Runs)
                        .First()
                        .IsWinner = true;
            }

            return new ScheduleGameModel
                   {
                       HeaderRow = GameHeaderRowFromGame(game),
                       RoadTeamRow = teamRows[0],
                       HomeTeamRow = teamRows[1]
                   };
        }

        private static ScheduleGameRowModel<IScheduleGameRowDataModel> GameHeaderRowFromGame(Game game)
        {
            return new ScheduleGameRowModel<IScheduleGameRowDataModel>
                   {
                       LabelData = new ScheduleGameHeaderModel
                                   {
                                       DisplayOutcome = game.GameStatus.DisplayOutcome,
                                       Outcome = game.GameStatus.Description,
                                       GameDate = game.GameDate,
                                       DisplayLocation = Consts.TournamentDates.Contains(game.GameDate.Date) && game.GameTypeId == GameType.PostSeason,
                                       Location = game.Location
                                   },
                       GameId = game.GameId,
                       DisplayScores = game.GameStatus.IsComplete,
                       Runs = "R",
                       Hits = "H"
                   };
        }

        private static ScheduleGameRowModel<IScheduleGameRowDataModel> GameTeamRowModelFromParticipant(GameParticipant gameParticipant)
        {
            return new ScheduleGameRowModel<IScheduleGameRowDataModel>
                   {
                       LabelData = new ScheduleGameTeamModel
                                   {
                                       Year = gameParticipant.Game.GameDate.Year,
                                       TeamId = gameParticipant.TeamYear.TeamId,
                                       TeamName = gameParticipant.TeamYear.FullName,
                                       RecordInfo = null // TODO: figure out a fast way to get this
                                   },
                       GameId = gameParticipant.GameId,
                       DisplayScores = gameParticipant.Game.GameStatus.IsComplete,
                       Runs = gameParticipant.RunsScored,
                       Hits = gameParticipant.StatLines.Any() ? gameParticipant.StatLines.Sum(sl => sl.StatHits) : (int?)null
                   };
        }

        private  DateTime GetClosestGameDate()
        {
            var dateQuery = from g in _dbContext.Games
                            orderby Math.Abs(SqlFunctions.DateDiff("day", g.GameDate, CentralTimeZone.Now).Value)
                            select g.GameDate;

            return dateQuery.First().Date;
        }

        private static GameBucket GetGameBucket(Game game)
        {
            if (Consts.TournamentDates.Contains(game.GameDate.Date))
            {
                return new GameBucket(game.GameDate.ToString("t"), (int)game.GameDate.TimeOfDay.TotalMinutes);
            }

            switch (game.GameTypeId)
            {
                case GameType.GamePlaceholder:
                    return new GameBucket("Post-Season", 0);
                case GameType.PostSeason:
                    return new GameBucket("Playoff", 0);
                case GameType.Exhibition:
                    return new GameBucket("Exhibition", 99);
            }

            var conferences = game.GameParticipants
                                  .Select(gp => gp.TeamYear.DivisionYear.ConferenceYear)
                                  .Distinct()
                                  .ToList();

            if (conferences.Count > 1)
            {
                return new GameBucket("Inter-Conference", 90);
            }

            return new GameBucket(conferences[0].Name, conferences[0].Sort);
        }

        public static GameBucket GetGameBucketForEdit(Game game, bool singleDate)
        {
            var sortBase = singleDate ? 0 : -game.GameDate.DayOfYear * 10000;
            var prefix = singleDate ? "" : game.GameDate.ToString("MMMM d, ");

            if (Consts.TournamentDates.Contains(game.GameDate.Date))
            {
                return new GameBucket($"{prefix}{game.GameDate:t}", sortBase + (int)game.GameDate.TimeOfDay.TotalMinutes);
            }

            switch (game.GameTypeId)
            {
                case GameType.GamePlaceholder:
                    return new GameBucket($"{prefix}Post-Season", sortBase + 9998);
                case GameType.PostSeason:
                    return new GameBucket($"{prefix}Playoff", sortBase + 9998);
                case GameType.Exhibition:
                    return new GameBucket($"{prefix}Exhibition", sortBase + 9999);
            }

            var conferences = game.GameParticipants
                                  .Select(gp => gp.TeamYear.DivisionYear.ConferenceYear)
                                  .Distinct()
                                  .ToList();

            if (conferences.Count > 1)
            {
                return new GameBucket($"{prefix}Inter-Conference", sortBase + 9990);
            }

            return new GameBucket($"{prefix}{conferences[0].Name}", sortBase + conferences[0].Sort);
        }

        /* JAF - This works, but is way too slow
        private static string GetRecordInfo(GameParticipant gameParticipant)
        {
            var teamGamesBeforeThisDate = (from gp in gameParticipant.TeamYear.GameParticipants
                                           where gp.Game.GameDate < gameParticipant.Game.GameDate.Date
                                                 && gp.Game.GameType.RecordGame
                                                 && gp.Game.GameStatus.IsComplete
                                           let opponent = gp.Game.GameParticipants.FirstOrDefault(gp2 => gp2.GameParticipantId != gp.GameParticipantId)
                                           select new
                                                  {
                                                      Won = gp.RunsScored > opponent.RunsScored,
                                                      Lost = gp.RunsScored < opponent.RunsScored,
                                                      Tied = gp.RunsScored == opponent.RunsScored,
                                                      IsHomeTeam = gp.IsHost
                                                  });

            var gamesByHosting = (from g in teamGamesBeforeThisDate
                                  group g by g.IsHomeTeam
                                  into gg
                                  select new
                                         {
                                             IsHomeTeam = gg.Key,
                                             Wins = gg.Count(g2 => g2.Won),
                                             Losses = gg.Count(g2 => g2.Lost),
                                             Ties = gg.Count(g2 => g2.Tied)
                                         }).ToList();

            var totalRecord = FormatRecord(gamesByHosting.Sum(g => g.Wins), gamesByHosting.Sum(g => g.Losses), gamesByHosting.Sum(g => g.Ties));

            var locationSpecificTotals = gamesByHosting.Single(g => g.IsHomeTeam == gameParticipant.IsHost);
            var locationSpecificRecord = FormatRecord(locationSpecificTotals.Wins, locationSpecificTotals.Losses, locationSpecificTotals.Ties);

            var locationSpecificLabel = gameParticipant.IsHost ? "Home" : "Away";

            return $"({totalRecord}, {locationSpecificRecord} {locationSpecificLabel})";
        }
        */

        public ScheduleModel GetScheduleModelForDate(DateTime? date)
        {
            var scheduleDate = date ?? GetClosestGameDate();

            var gamesOnDate = _dbContext.Games
                                       .Where(g => SqlFunctions.DateDiff("day", g.GameDate, scheduleDate) == 0)
                                       .ToList();

            if (!gamesOnDate.Any())
            {
                return null;
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

            return new ScheduleModel
                   {
                       Date = scheduleDate,
                       ConferenceModels = bucketedGames.OrderBy(kvp => kvp.Key.Sort)
                                                       .Select(kvp => new ScheduleConferenceModel
                                                                      {
                                                                          Label = kvp.Key.Label,
                                                                          Games = kvp.Value
                                                                                     .OrderBy(g => g.GameDate)
                                                                                     .ThenBy(g => g.Location)
                                                                                     .Select(GameModelFromGame)
                                                                                     .ToList()
                                                                      })
                                                       .ToList()
                   };
        }

        public List<TeamGameModel> GetTeamSchedule(TeamYear teamYear)
        {
            return (from gp in teamYear.GameParticipants
                    let opponent = gp.Game.GameParticipants.FirstOrDefault(gp2 => gp2.GameParticipantId != gp.GameParticipantId)
                    let won = gp.RunsScored > opponent.RunsScored
                    let lost = gp.RunsScored < opponent.RunsScored
                    let winnerRuns = won ? gp.RunsScored : opponent.RunsScored
                    let loserRuns = lost ? gp.RunsScored : opponent.RunsScored
                    let winLossChar = (won ? "W" : lost ? "L" : "T")
                                      + (gp.Game.GameStatusId == GameStatus.Forfeited ? " (F)" : string.Empty)
                    orderby gp.Game.GameDate
                    select new TeamGameModel
                           {
                               GameId = gp.Game.GameId,
                               Date = gp.Game.GameDate,
                               OpponentId = opponent.TeamYear.TeamId,
                               OpponentName = opponent.TeamYear.FullName + (gp.Game.GameTypeId == GameType.Exhibition ? " *" : string.Empty),
                               Location = gp.Game.GameStatus.IsComplete ? null : gp.Game.Location,
                               IsGameCompleted = gp.Game.GameStatus.IsComplete,
                               DidWin = won,
                               DidLose = lost,
                               IsHomeTeam = gp.IsHost,
                               IsNeutralSite = gp.Game.GameTypeId == GameType.GamePlaceholder
                                               || gp.Game.GameTypeId == GameType.PostSeason,
                               IsPlaceholder = gp.Game.GameTypeId == GameType.GamePlaceholder,
                               IsExhibition = gp.Game.GameTypeId == GameType.Exhibition,
                               GameResultDescription = gp.Game.GameStatus.IsComplete
                                                           ? winLossChar + " " + winnerRuns + "-" + loserRuns
                                                           : gp.Game.GameStatusId != GameStatus.Scheduled
                                                                 ? gp.Game.GameStatus.Description
                                                                 : string.Empty
                           })
                .Concat(_dbContext.Games
                                  .Where(g => g.GameDate.Year == teamYear.Year
                                                && g.GameTypeId == GameType.GamePlaceholder)
                                  .ToList()
                                  .Select(g => new TeamGameModel
                                              {
                                                  GameId = g.GameId,
                                                  Date = g.GameDate,
                                                  OpponentName = g.HomeParticipant.TeamYear.FullName,
                                                  IsNeutralSite = true,
                                                  IsPlaceholder = true
                                              }))
                .ToList();
        }

        public YearCalendarModel GetYearCalendarModel(int year, DateTime activeDate)
        {
            var model = new YearCalendarModel
                        {
                            Year = year,
                            ActiveDate = activeDate,
                            Months = new List<YearCalendarMonthModel>()
                        };

            var gameDatesInYear = (from g in _dbContext.Games
                                   where g.GameDate.Year == year
                                   select g.GameDate)
                .DistinctBy(g => g.Date)
                .Select(d => d.Date)
                .ToList();

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

            return model;
        }
    }
}