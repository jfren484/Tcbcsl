using System;
using System.Collections.Generic;
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
                       DisplayOutcome = game.GameStatus.DisplayOutcome,
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
                       Year = gameParticipant.Game.GameDate.Year,
                       IsWinner = gameParticipant.RunsScored > opponent.RunsScored,
                       RecordInfo = null, // TODO: figure out a fast way to get this
                       RunsScored = gameParticipant.RunsScored,
                       Hits = gameParticipant.StatLines.Any() ? gameParticipant.StatLines.Sum(sl => sl.StatHits) : (int?)null
                   };
        }

        public static List<TeamGameModel> GetTeamSchedule(TeamYear teamYear)
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
                        OpponentName = opponent.TeamYear.Church.DisplayName
                                              + (string.IsNullOrEmpty(opponent.TeamYear.TeamName)
                                                     ? string.Empty
                                                     : " " + opponent.TeamYear.TeamName)
                                              + (gp.Game.GameTypeId == GameType.Exhibition ? " *" : string.Empty),
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
                .ToList();
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
    }
}