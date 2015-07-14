using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    public class TeamController : ControllerBase
    {
        [Route("Teams/{year:year?}")]
        public ActionResult Teams(int year = Consts.CurrentYear)
        {
            var divisions = DbContext
                .DivisionYears
                .Where(dy => dy.Year == year && dy.IsInLeague)
                .OrderBy(dy => dy.ConferenceYear.Sort)
                .ThenBy(dy => dy.Sort)
                .Select(dy => new DivisionTeamsModel
                              {
                                  DivisionName = dy.Name,
                                  Teams = dy.TeamYears
                                            .Select(ty => new TeamsListTeamModel
                                                          {
                                                              TeamId = ty.TeamId,
                                                              TeamName = string.IsNullOrEmpty(ty.TeamName)
                                                                             ? ty.Church.DisplayName
                                                                             : ty.Church.DisplayName + " " + ty.TeamName
                                                          })
                                            .ToList()
                              })
                .ToList();

            return View(new TeamsListModel
                        {
                            Year = year,
                            Divisions = divisions
                        });
        }

        [Route("Team/{teamId}/{year:year?}")]
        public ActionResult View(int teamId, int year = Consts.CurrentYear)
        {
            var statsCategories = new List<StatsCategory>
                                  {
                                      new StatsCategory {Name = "AVG", IsPercentage = true},
                                      new StatsCategory {Name = "HR"},
                                      new StatsCategory {Name = "BB"},
                                      new StatsCategory {Name = "RBI"},
                                      new StatsCategory {Name = "Runs"},
                                      new StatsCategory {Name = "OPS", IsPercentage = true}
                                  };

            var teamInfo = DbContext
                .TeamYears
                .Select(ty => new
                              {
                                  TeamViewModel = new TeamViewModel
                                                  {
                                                      TeamId = ty.TeamId,
                                                      Year = ty.Year,
                                                      TeamName = string.IsNullOrEmpty(ty.TeamName)
                                                                     ? ty.Church.DisplayName
                                                                     : ty.Church.DisplayName + " " + ty.TeamName,
                                                      DivisionName = ty.DivisionYear.Name,
                                                      ChurchId = ty.ChurchId,
                                                      ChurchName = ty.Church.FullName,
                                                      CoachId = ty.HeadCoachId,
                                                      CoachName = ty.HeadCoach.FirstName + " " + ty.HeadCoach.LastName,
                                                      Field = ty.Team.FieldInformation,
                                                      Comments = ty.Team.Comments,
                                                      NewsItems = (from item in ty.Team.NewsItems
                                                                   where item.IsActive
                                                                         && item.StartDate < DateTime.Now
                                                                         && item.EndDate > DateTime.Now
                                                                   orderby item.StartDate descending
                                                                   select new NewsItemViewModel
                                                                          {
                                                                              StartDate = item.StartDate,
                                                                              Subject = item.Subject,
                                                                              Content = item.Content
                                                                          })
                                                          .ToList(),
                                                      Schedule = (from gp in ty.GameParticipants
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
                                                          .ToList()

                                                  },
                                  TeamGamesThreshhold = ty.GameParticipants.Count(gp => gp.StatLines.Any())
                              })
                .Single(ti => ti.TeamViewModel.TeamId == teamId && ti.TeamViewModel.Year == year);

            var model = teamInfo.TeamViewModel;
            model.StatsLeaders = statsCategories
                .Select(cat => GetStatsCategoryLeader(cat, model.TeamId, year, teamInfo.TeamGamesThreshhold))
                .ToList();

            return View(model);
        }

        private StatsLeaderModel GetStatsCategoryLeader(StatsCategory category, int teamId, int year, int teamGamesThreshhold)
        {
            var playerStats = KeyStatsForTeam(teamId, year);
            if (category.IsPercentage)
            {
                playerStats = playerStats.Where(ps => ps.Games >= teamGamesThreshhold);
            }

            var selectedValues = playerStats
                .Select(ps => new
                              {
                                  ps.PlayerId,
                                  ps.Name,
                                  Value = category.Name == "AVG"
                                              ? ps.AVG
                                              : category.Name == "HR"
                                                    ? ps.HR
                                                    : category.Name == "BB"
                                                          ? ps.BB
                                                          : category.Name == "RBI"
                                                                ? ps.RBI
                                                                : category.Name == "Runs"
                                                                      ? ps.Runs
                                                                      : ps.OPS
                              });

            var topValue = selectedValues
                .GroupBy(val => val.Value)
                .OrderByDescending(group => group.Key)
                .FirstOrDefault();

            var leader = new StatsLeaderModel
                         {
                             Category = category,
                             PlayerId = topValue == null || topValue.Key == 0 || topValue.Count() > 1 ? (int?)null : topValue.Single().PlayerId,
                             Name = topValue == null || topValue.Key == 0
                                        ? null
                                        : topValue.Count() > 1
                                              ? topValue.Count() + " tied"
                                              : topValue.Single().Name,
                             Value = topValue == null || topValue.Key == 0 ? (decimal?)null : topValue.Key
                         };

            return leader;
        }

        private class PlayerStats
        {
            // ReSharper disable InconsistentNaming
            public int PlayerId { get; set; }
            public string Name { get; set; }
            public int Games { get; set; }
            public decimal AVG { get; set; }
            public int HR { get; set; }
            public int BB { get; set; }
            public int RBI { get; set; }
            public int Runs { get; set; }
            public decimal OPS { get; set; }
            // ReSharper restore InconsistentNaming
        }

        private IQueryable<PlayerStats> KeyStatsForTeam(int teamId, int year)
        {
            return from line in DbContext.StatLines
                   where line.GameParticipant.TeamYear.TeamId == teamId
                         && line.GameParticipant.TeamYear.Year == year
                   group line by line.Player
                   into lineGroup
                   select new PlayerStats
                          {
                              PlayerId = lineGroup.Key.PlayerId,
                              Name = (lineGroup.Key.NameFirst + " " + lineGroup.Key.NameLast).Trim(),
                              Games = lineGroup.Count(),
                              AVG = lineGroup.Sum(line => line.StatHits) / (decimal)lineGroup.Sum(line => line.StatAtBats),
                              HR = lineGroup.Sum(line => line.StatHomeRuns),
                              BB = lineGroup.Sum(line => line.StatWalks),
                              RBI = lineGroup.Sum(line => line.StatRunsBattedIn),
                              Runs = lineGroup.Sum(line => line.StatRuns),
                              OPS = lineGroup.Sum(line => line.StatHits + line.StatWalks) / (decimal)lineGroup.Sum(line => line.StatPlateAppearances) +
                                    lineGroup.Sum(line => line.StatTotalBases) / (decimal)lineGroup.Sum(line => line.StatAtBats)
                          };
        }
    }
}