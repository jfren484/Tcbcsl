using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    public class StandingsController : ControllerBase
    {
        [Route("Standings/{type}/{year:year?}")]
        public ActionResult Standings(StandingsType type, int year = Consts.CurrentYear)
        {
            var teams = GetTeamRawModels(year);

            var model = new StandingsModel
            {
                Year = year,
                Type = type,
                ShowTies = teams.Any(t => t.Model.Ties > 0),
                Groups = type == StandingsType.Division
                    ? GetDivisionStandingsTeamModels(teams)
                    : GetLeagueStandingsTeamModels(teams)
            };

            return View(model);
        }

        #region GetTeamRawModels (and helpers)

        private List<TeamStandingsRaw> GetTeamRawModels(int year)
        {
            var games = GetGameRawModels(year);

            var teamModels = (from g in games
                              group g by new { g.DivisionYearId, g.TeamId } into gg
                              let wins = gg.Count(g => g.Outcome > 0)
                              let losses = gg.Count(g => g.Outcome < 0)
                              let ties = gg.Count(g => g.Outcome == 0)
                              let last5GameInfo = gg.OrderByDescending(g => g.GameDate).Take(5)
                              let first = gg.FirstOrDefault()
                              select new TeamStandingsRaw
                              {
                                  Model = new StandingsTeamModel
                                  {
                                      TeamId = gg.Key.TeamId,
                                      TeamName = first.ChurchName + (string.IsNullOrEmpty(first.TeamName)
                                          ? null
                                          : " " + first.TeamName),
                                      Wins = wins,
                                      Losses = losses,
                                      Ties = ties,
                                      WinningPercentage = (double)wins / (wins + losses + ties),
                                      GamesBack = (double)(wins - losses) / 2,
                                      DivisionWins = gg.Count(g => g.IsDivision && g.Outcome > 0),
                                      DivisionLosses = gg.Count(g => g.IsDivision && g.Outcome < 0),
                                      DivisionTies = gg.Count(g => g.IsDivision && g.Outcome == 0),
                                      RunsScored = gg.Sum(g => g.RunsScored),
                                      RunsAllowed = gg.Sum(g => g.RunsAllowed),
                                      Last5Wins = last5GameInfo.Count(g => g.Outcome > 0),
                                      Last5Losses = last5GameInfo.Count(g => g.Outcome < 0),
                                      Last5Ties = last5GameInfo.Count(g => g.Outcome == 0),
                                      StreakOutcome = gg.OrderByDescending(g => g.GameDate).FirstOrDefault().Outcome
                                  },
                                  DivisionYearId = gg.Key.DivisionYearId,
                                  Record = gg.OrderByDescending(g => g.GameDate)
                                             .Select(g => g.Outcome)
                                             .ToList()
                              }).ToList();

            teamModels.ForEach(UpdateTeamStreaks);

            return teamModels;
        }

        private IQueryable<GameRaw> GetGameRawModels(int year)
        {
            return from gp1 in DbContext.GameParticipants
                   let g = gp1.Game
                   where g.GameDate.Year == year &&
                       g.GameTypeId == GameType.RegularSeason &&
                       g.GameStatus.IsComplete
                   let gp2 = g.GameParticipants.FirstOrDefault(gp => gp.GameParticipantId != gp1.GameParticipantId)
                   select new GameRaw
                   {
                       DivisionYearId = gp1.TeamYear.DivisionYearId,
                       TeamId = gp1.TeamYear.TeamId,
                       TeamName = gp1.TeamYear.TeamName,
                       ChurchName = gp1.TeamYear.Church.DisplayName,
                       GameDate = gp1.Game.GameDate,
                       RunsScored = gp1.RunsScored,
                       RunsAllowed = gp2.RunsScored,
                       Outcome = gp1.RunsScored.CompareTo(gp2.RunsScored),
                       IsDivision = gp1.TeamYear.DivisionYear.DivisionId == gp2.TeamYear.DivisionYear.DivisionId
                   };
        }

        private void UpdateTeamStreaks(TeamStandingsRaw teamRawModel)
        {
            var streakCount = 1;

            while (streakCount < teamRawModel.Record.Count &&
                   teamRawModel.Record[streakCount] == teamRawModel.Model.StreakOutcome)
            {
                ++streakCount;
            }

            teamRawModel.Model.StreakCount = streakCount;
        }

        #endregion

        #region GetXXXStandingsTeamModels (and helpers)

        private List<StandingsGroupModel> GetDivisionStandingsTeamModels(List<TeamStandingsRaw> teams)
        {
            var models = (from t in teams
                          group t by t.DivisionYearId into dg
                          join dy in DbContext.DivisionYears on dg.Key equals dy.DivisionYearId
                          orderby dy.ConferenceYear.Sort, dy.Sort
                          select new StandingsGroupModel
                          {
                              Name = dy.Name,
                              Teams = GetTeamsForGroup(dg)
                          }).ToList();

            models.ForEach(UpdateGamesBack);

            return models;
        }

        private List<StandingsGroupModel> GetLeagueStandingsTeamModels(List<TeamStandingsRaw> teams)
        {
            var allTeamsModel = new StandingsGroupModel
            {
                Name = "All Teams",
                Teams = GetTeamsForGroup(teams)
            };

            UpdateGamesBack(allTeamsModel);

            return new List<StandingsGroupModel> { allTeamsModel };
        }

        private List<StandingsTeamModel> GetTeamsForGroup(IEnumerable<TeamStandingsRaw> teamRawModels)
        {
            return teamRawModels.Select(t => t.Model)
                                .OrderByDescending(t => t.GamesBack)
                                .ThenByDescending(t => t.WinningPercentage)
                                .ToList();
        }

        private void UpdateGamesBack(StandingsGroupModel groupModel)
        {
            var gbDiff = groupModel.Teams.First().GamesBack;
            groupModel.Teams.ForEach(t =>
            {
                t.GamesBack -= gbDiff;
            });
        }

        #endregion

        #region Helper Classes

        private class GameRaw
        {
            public int DivisionYearId { get; set; }
            public int TeamId { get; set; }
            public string TeamName { get; set; }
            public string ChurchName { get; set; }
            public DateTime GameDate { get; set; }
            public int RunsScored { get; set; }
            public int RunsAllowed { get; set; }
            public int Outcome { get; set; }
            public bool IsDivision { get; set; }
        }

        private class TeamStandingsRaw
        {
            public StandingsTeamModel Model { get; set; }
            public int DivisionYearId { get; set; }
            public List<int> Record { get; set; }
        }

        #endregion
    }
}