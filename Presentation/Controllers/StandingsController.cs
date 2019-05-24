using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Tcbcsl.Data;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    public class StandingsController : TcbcslControllerBase
    {
        public StandingsController(TcbcslDbContext dbContext) : base(dbContext) { }

        [Route("Standings/{type}/{year:year?}")]
        public ActionResult Standings(StandingsType type, int year = Consts.CurrentYear)
        {
            var teams = GetStandingsModels(year);

            var model = new StandingsModel
                        {
                            Year = year,
                            Type = type,
                            ShowTies = teams.Any(t => t.Ties > 0),
                            Groups = type == StandingsType.Division
                                         ? GetDivisionStandingsTeamModels(teams)
                                         : GetLeagueStandingsTeamModels(teams)
                        };

            return View(model);
        }

        #region GetTeamRawModels (and helpers)

        private List<StandingsTeamModel> GetStandingsModels(int year)
        {
            return (from ty in DbContext.TeamYears
                    where ty.Year == year && ty.DivisionYear.IsInLeague
                    let games = from gp in ty.GameParticipants
                                where gp.Game.GameTypeId == GameType.RegularSeason &&
                                      gp.Game.GameStatus.IsComplete
                                let opponent = gp.Game.GameParticipants.FirstOrDefault(gp2 => gp2.GameParticipantId != gp.GameParticipantId)
                                select new
                                {
                                    gp.Game.GameDate,
                                    gp.RunsScored,
                                    RunsAllowed = opponent.RunsScored,
                                    Outcome = gp.RunsScored.CompareTo(opponent.RunsScored),
                                    IsDivision = gp.TeamYear.DivisionYear.DivisionId == opponent.TeamYear.DivisionYear.DivisionId
                                }
                    let wins = games.Count(g => g.Outcome > 0)
                    let losses = games.Count(g => g.Outcome < 0)
                    let ties = games.Count(g => g.Outcome == 0)
                    let last5GameInfo = games.OrderByDescending(g => g.GameDate).Take(5)
                    let first = games.FirstOrDefault()
                    select new
                    {
                        StandingsTeamModel = new StandingsTeamModel
                        {
                            Year = year,
                            DivisionYearId = ty.DivisionYearId,
                            TeamId = ty.TeamId,
                            TeamName = ty.FullName,
                            ClinchChar = ty.Clinch,
                            Wins = wins,
                            Losses = losses,
                            Ties = ties,
                            WinningPercentage = games.Any() ? (double)wins / (wins + losses + ties) : 0,
                            GamesBack = (double)(wins - losses) / 2,
                            DivisionWins = games.Count(g => g.IsDivision && g.Outcome > 0),
                            DivisionLosses = games.Count(g => g.IsDivision && g.Outcome < 0),
                            DivisionTies = games.Count(g => g.IsDivision && g.Outcome == 0),
                            RunsScored = games.Any() ? games.Sum(g => g.RunsScored) : 0,
                            RunsAllowed = games.Any() ? games.Sum(g => g.RunsAllowed) : 0,
                            Last5Wins = last5GameInfo.Count(g => g.Outcome > 0),
                            Last5Losses = last5GameInfo.Count(g => g.Outcome < 0),
                            Last5Ties = last5GameInfo.Count(g => g.Outcome == 0),
                            StreakOutcome = games.Any() ? games.OrderByDescending(g => g.GameDate).FirstOrDefault().Outcome : 0
                        },
                        Record = games.OrderByDescending(g => g.GameDate)
                                                               .Select(g => g.Outcome)
                                                               .ToList()
                    })
                    .ToList()
                    .Select(data =>
                        {
                            data.StandingsTeamModel.StreakCount = 0;

                            while (data.StandingsTeamModel.StreakCount < data.Record.Count &&
                                   data.Record[data.StandingsTeamModel.StreakCount] == data.StandingsTeamModel.StreakOutcome)
                            {
                                ++data.StandingsTeamModel.StreakCount;
                            }

                            return data.StandingsTeamModel;
                        })
                    .ToList();
        }

        #endregion

        #region GetXXXStandingsTeamModels (and helpers)

        private List<StandingsGroupModel> GetDivisionStandingsTeamModels(List<StandingsTeamModel> teams)
        {
            var models = (from t in teams
                          group t by t.DivisionYearId
                          into dg
                          join dy in DbContext.DivisionYears on dg.Key equals dy.DivisionYearId
                          orderby dy.ConferenceYear.Sort, dy.Sort
                          select new StandingsGroupModel
                                 {
                                     Name = $"{dy.Name} Division",
                                     Teams = GetTeamsForGroup(dg)
                                 }).ToList();

            models.ForEach(UpdateGamesBack);

            return models;
        }

        private static List<StandingsGroupModel> GetLeagueStandingsTeamModels(List<StandingsTeamModel> teams)
        {
            var allTeamsModel = new StandingsGroupModel
                                {
                                    Name = "All Teams",
                                    Teams = GetTeamsForGroup(teams)
                                };

            UpdateGamesBack(allTeamsModel);

            return new List<StandingsGroupModel> {allTeamsModel};
        }

        private static List<StandingsTeamModel> GetTeamsForGroup(IEnumerable<StandingsTeamModel> teamRawModels)
        {
            return teamRawModels.OrderByDescending(t => t.GamesBack)
                                .ThenByDescending(t => t.WinningPercentage)
                                .ToList();
        }

        private static void UpdateGamesBack(StandingsGroupModel groupModel)
        {
            var gbDiff = groupModel.Teams.Max(t => t.GamesBack);
            groupModel.Teams.ForEach(t =>
                                     {
                                         t.GamesBack -= gbDiff;
                                     });
        }

        #endregion
    }
}