using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    [RoutePrefix("Statistics")]
    public class StatisticsController : ControllerBase
    {
        [Route("")]
        public ActionResult Statistics(StatisticsFilterModel filterModel)
        {
            return View(filterModel);
        }

        #region Game

        [Route("Game/{gameId}")]
        public ActionResult StatisticsForGame(int gameId)
        {
            var game = DbContext.Games.SingleOrDefault(g => g.GameId == gameId);
            if (game == null)
            {
                return HttpNotFound();
            }

            var teams = game.GameParticipants
                            .OrderBy(gp => gp.IsHost)
                            .Select(gp => new GameStatisticsTeamModel
                                          {
                                              GameParticipantId = gp.GameParticipantId,
                                              HostLabel = gp.IsHost ? "Home" : "Road",
                                              TeamId = gp.TeamYear.TeamId,
                                              Year = gp.TeamYear.Year,
                                              TeamName = gp.TeamYear.FullName
                                          })
                            .ToList();

            var model = new GameStatisticsModel
                        {
                            GameDate = game.GameDate,
                            RoadTeam = teams[0],
                            HomeTeam = teams[1]
                        };

            return View(model);
        }

        [Route("GameData/{gameParticipantId}")]
        public ActionResult GameData(int gameParticipantId)
        {
            var gameParticipant = DbContext.GameParticipants.SingleOrDefault(gp => gp.GameParticipantId == gameParticipantId);
            if (gameParticipant == null)
            {
                return HttpNotFound();
            }

            var data = gameParticipant.StatLines
                                      .OrderBy(sl => sl.BattingOrderPosition)
                                      .Select(sl => new GamePlayerStatisticsModel
                                                    {
                                                        PlayerName = sl.Player.FullName,
                                                        PlateAppearances = sl.StatPlateAppearances,
                                                        AtBats = sl.StatAtBats,
                                                        Hits = sl.StatHits,
                                                        TotalBases = sl.StatTotalBases,
                                                        Runs = sl.StatRuns,
                                                        RunsBattedIn = sl.StatRunsBattedIn,
                                                        Singles = sl.StatSingles,
                                                        Doubles = sl.StatDoubles,
                                                        Triples = sl.StatTriples,
                                                        HomeRuns = sl.StatHomeRuns,
                                                        Walks = sl.StatWalks,
                                                        SacrificeFlies = sl.StatSacrificeFlies,
                                                        Outs = sl.StatOuts,
                                                        FieldersChoices = sl.StatFieldersChoices,
                                                        ReachedByErrors = sl.StatReachedByErrors,
                                                        Strikeouts = sl.StatStrikeouts,
                                                    });

            return Json(data);
        }

        #endregion

        #region Team

        [Route("Team/{teamId}/{year:year?}")]
        public ActionResult StatisticsForTeam(int teamId, int year = Consts.CurrentYear, string sort = null)
        {
            var teamYear = DbContext.TeamYears.SingleOrDefault(ty => ty.TeamId == teamId && ty.Year == year);
            if (teamYear == null)
            {
                return HttpNotFound();
            }

            var model = new TeamStatisticsModel
            {
                TeamYearId = teamYear.TeamYearId,
                Year = year,
                TeamName = teamYear.FullName,
                SortColumn = sort
            };

            return View(model);
        }

        [Route("TeamData/{teamYearId}")]
        public ActionResult TeamData(int teamYearId)
        {
            var teamYear = DbContext.TeamYears.SingleOrDefault(ty => ty.TeamYearId == teamYearId);
            if (teamYear == null)
            {
                return HttpNotFound();
            }

            var data = teamYear.GameParticipants
                               .SelectMany(gp => gp.StatLines)
                               .GroupBy(sl => new {sl.PlayerId, sl.Player.FullName})
                               .Select(slg => new TeamPlayerStatisticsModel
                                              {
                                                  PlayerName = slg.Key.FullName,
                                                  Games = slg.Count(),
                                                  PlateAppearances = slg.Sum(sl => sl.StatPlateAppearances),
                                                  AtBats = slg.Sum(sl => sl.StatAtBats),
                                                  Hits = slg.Sum(sl => sl.StatHits),
                                                  TotalBases = slg.Sum(sl => sl.StatTotalBases),
                                                  Runs = slg.Sum(sl => sl.StatRuns),
                                                  RunsBattedIn = slg.Sum(sl => sl.StatRunsBattedIn),
                                                  Singles = slg.Sum(sl => sl.StatSingles),
                                                  Doubles = slg.Sum(sl => sl.StatDoubles),
                                                  Triples = slg.Sum(sl => sl.StatTriples),
                                                  HomeRuns = slg.Sum(sl => sl.StatHomeRuns),
                                                  Walks = slg.Sum(sl => sl.StatWalks),
                                                  SacrificeFlies = slg.Sum(sl => sl.StatSacrificeFlies),
                                                  Outs = slg.Sum(sl => sl.StatOuts),
                                                  FieldersChoices = slg.Sum(sl => sl.StatFieldersChoices),
                                                  ReachedByErrors = slg.Sum(sl => sl.StatReachedByErrors),
                                                  Strikeouts = slg.Sum(sl => sl.StatStrikeouts),
                                              });

            return Json(data);
        }

        #endregion
    }
}