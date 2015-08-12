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
                                              TeamName = gp.TeamYear.Church.DisplayName + " " + gp.TeamYear.TeamName
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
                                                        PlayerName = sl.Player.NameFirst + " " + sl.Player.NameLast,
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
    }
}