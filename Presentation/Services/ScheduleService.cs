using System.Linq;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Services
{
    public static class ScheduleService
    {
        public static string FormatRecord(int wins, int losses, int ties)
        {
            return $"{wins}-{losses}{ties:'-'#;;''}";
        }

        public static ScheduleGameModel GameModelFromGame(Game game)
        {
            return new ScheduleGameModel
                   {
                       GameId = game.GameId,
                       GameDate = game.GameDate,
                       DisplayScores = game.GameStatus.DisplayScores,
                       IsComplete = game.GameStatus.IsComplete,
                       Outcome = game.GameStatus.Description,
                       Teams = game.GameParticipants
                                   .OrderBy(gp => gp.IsHost)
                                   .Select(GameTeamModelFromParticipant)
                                   .ToList()
                   };
        }

        private static ScheduleGameTeamModel GameTeamModelFromParticipant(GameParticipant gameParticipant)
        {
            var opponent = gameParticipant.Game.GameParticipants.Single(gp2 => gp2.GameParticipantId != gameParticipant.GameParticipantId);

            return new ScheduleGameTeamModel
                   {
                       TeamId = gameParticipant.TeamYear.TeamId,
                       TeamName = gameParticipant.TeamYear.Church.DisplayName + (string.IsNullOrEmpty(gameParticipant.TeamYear.TeamName)
                                                                                     ? null
                                                                                     : " " + gameParticipant.TeamYear.TeamName),
                       IsWinner = gameParticipant.RunsScored > opponent.RunsScored,
                       RecordInfo = "(0-0, 0-0 Anywhere)",// GetRecordInfo(gameParticipant), // JAF - this is just too slow right now
                       RunsScored = gameParticipant.RunsScored,
                       Hits = gameParticipant.StatLines.Any() ? gameParticipant.StatLines.Sum(sl => sl.StatHits) : (int?)null
                   };
        }

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
    }
}