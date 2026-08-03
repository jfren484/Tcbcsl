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
        public ActionResult Standings(StandingsType type, int? year)
        {
            var teams = GetStandingsModels(year ?? Consts.CurrentYear);

            var model = new StandingsModel
                        {
                            Year = year ?? Consts.CurrentYear,
                            Type = type,
                            ShowTies = teams.Any(t => t.TotalTies > 0),
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
                    let divGames = games.Where(g => g.IsDivision)
                    let totWins = games.Count(g => g.Outcome > 0)
                    let totLosses = games.Count(g => g.Outcome < 0)
                    let totTies = games.Count(g => g.Outcome == 0)
                    let divWins = divGames.Count(g => g.Outcome > 0)
                    let divLosses = divGames.Count(g => g.Outcome < 0)
                    let divTies = divGames.Count(g => g.Outcome == 0)
                    let last5GameInfo = divGames.OrderByDescending(g => g.GameDate).Take(5)
                    select new
                    {
                        StandingsTeamModel = new StandingsTeamModel
                        {
                            Year = year,
                            DivisionYearId = ty.DivisionYearId,
                            TeamId = ty.TeamId,
                            TeamName = ty.FullName,
                            ClinchChar = ty.Clinch,
                            TotalWins = totWins,
                            TotalLosses = totLosses,
                            TotalTies = totTies,
                            DivisionWinningPercentage = divGames.Any() ? ((double)divWins + (double)divTies / 2) / (divWins + divLosses + divTies) : 0,
                            DivisionGamesBack = (double)(divWins - divLosses) / 2,
                            DivisionWins = divWins,
                            DivisionLosses = divLosses,
                            DivisionTies = divTies,
                            DivisionRunsScored = divGames.Any() ? games.Sum(g => g.RunsScored) : 0,
                            DivisionRunsAllowed = divGames.Any() ? games.Sum(g => g.RunsAllowed) : 0,
                            Last5Wins = last5GameInfo.Count(g => g.Outcome > 0),
                            Last5Losses = last5GameInfo.Count(g => g.Outcome < 0),
                            Last5Ties = last5GameInfo.Count(g => g.Outcome == 0),
                            StreakOutcome = divGames.Any() ? divGames.OrderByDescending(g => g.GameDate).FirstOrDefault().Outcome : 0
                        },
                        DivRecord = divGames.OrderByDescending(g => g.GameDate)
                                            .Select(g => g.Outcome)
                                            .ToList()
                    })
                    .ToList()
                    .Select(data =>
                        {
                            data.StandingsTeamModel.StreakCount = 0;

                            while (data.StandingsTeamModel.StreakCount < data.DivRecord.Count &&
                                   data.DivRecord[data.StandingsTeamModel.StreakCount] == data.StandingsTeamModel.StreakOutcome)
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
            return teamRawModels.OrderByDescending(t => t.DivisionGamesBack)
                                .ThenByDescending(t => t.DivisionWinningPercentage)
                                .ThenByDescending(t => t.DivisionRunsScored - t.DivisionRunsAllowed)
                                .ToList();
        }

        private static void UpdateGamesBack(StandingsGroupModel groupModel)
        {
            var gbDiff = groupModel.Teams.Max(t => t.DivisionGamesBack);
            groupModel.Teams.ForEach(t =>
                                     {
                                         t.DivisionGamesBack -= gbDiff;
                                     });
        }

        #endregion
    }
}