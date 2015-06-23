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
        public ActionResult Standings(StandingsType type, int year = Config.CurrentYear)
        {
            var games = from gp1 in DbContext.GameParticipants
                        let g = gp1.Game
                        where g.GameDate.Year == year &&
                            g.GameTypeId == GameType.RegularSeason &&
                            g.GameStatus.IsComplete
                        let gp2 = g.GameParticipants.FirstOrDefault(gp => gp.GameParticipantId != gp1.GameParticipantId)
                        select new
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

            var teams = (from g in games
                         group g by new { g.DivisionYearId, g.TeamId } into gg
                         let wins = gg.Count(g => g.Outcome > 0)
                         let losses = gg.Count(g => g.Outcome < 0)
                         let ties = gg.Count(g => g.Outcome == 0)
                         let last5GameInfo = gg.OrderByDescending(g => g.GameDate).Take(5)
                         let first = gg.FirstOrDefault()
                         select new
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

            foreach (var t in teams)
            {
                for (t.Model.StreakCount = 1; t.Model.StreakCount < t.Record.Count && t.Record[t.Model.StreakCount] == t.Model.StreakOutcome; ++t.Model.StreakCount)
                {
                    // Nothing to do - all logic is in for()
                }
            }

            var model = new StandingsModel
            {
                Year = year,
                Type = type
            };

            if (type == StandingsType.Division)
            {
                model.Groups = (from t in teams
                                group t by t.DivisionYearId into dg
                                join dy in DbContext.DivisionYears on dg.Key equals dy.DivisionYearId
                                orderby dy.ConferenceYear.Sort, dy.Sort
                                select new StandingsGroupModel
                                {
                                    Name = dy.Name,
                                    Teams = dg.Select(g => g.Model)
                                              .OrderBy(g => g.GamesBack)
                                              .ThenByDescending(g => g.WinningPercentage)
                                              .ToList()
                                }).ToList();
            }
            else
            {
                model.Groups = new List<StandingsGroupModel>
                    {
                        new StandingsGroupModel
                        {
                            Name = "All Teams",
                            Teams = teams.Select(g => g.Model)
                                                  .OrderBy(g => g.GamesBack)
                                                  .ThenByDescending(g => g.WinningPercentage)
                                                  .ToList()
                        }
                    };
            }

            return View(model);
        }
    }
}