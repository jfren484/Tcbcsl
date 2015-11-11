using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    [RoutePrefix("Statistics")]
    public class StatisticsController : ControllerBase
    {
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
                            .Select(gp => new GameStatisticsPageTeamModel
                                          {
                                              GameParticipantId = gp.GameParticipantId,
                                              HostLabel = gp.IsHost ? "Home" : "Road",
                                              Year = gp.TeamYear.Year,
                                              TeamInfo = new StatisticsTeamInfoModel
                                                     {
                                                         TeamId = gp.TeamYear.TeamId,
                                                         TeamName = gp.TeamYear.FullName
                                                     }
                                          })
                            .ToList();

            var model = new GameStatisticsPageModel
                        {
                            GameDate = game.GameDate,
                            RoadTeam = teams[0],
                            HomeTeam = teams[1]
                        };

            return View(model);
        }

        [HttpPost]
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
                                      .Select(sl => new GamePlayerStatisticsRowModel
                                                    {
                                                        Year = (YearEnum)gameParticipant.Game.GameDate.Year,
                                                        Player = new StatisticsPlayerInfoModel
                                                                 {
                                                                     PlayerId = sl.PlayerId,
                                                                     PlayerFirstName = sl.Player.NameFirst,
                                                                     PlayerLastName = sl.Player.NameLast
                                                                 },
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
                                                        Strikeouts = sl.StatStrikeouts
                                                    });
            return Json(data);
        }

        #endregion

        #region League Individual

        [Route("League/Individual/{year:years?}")]
        public ActionResult StatisticsForLeagueIndividual(YearEnum year = (YearEnum)Consts.CurrentYear)
        {
            var model = new LeagueStatisticsPageModel
            {
                Year = year
            };

            return View(model);
        }

        [HttpPost]
        [Route("LeagueData/Individual/{year:years}")]
        public ActionResult LeagueIndividualData(YearEnum year)
        {
            var statLines = DbContext.StatLines.AsQueryable();

            if (year != YearEnum.All)
            {
                statLines = statLines.Where(sl => sl.GameParticipant.Game.GameDate.Year == (int)year);
            }

            var data = statLines
                .GroupBy(sl => new
                               {
                                   PlayerId = sl.PlayerId,
                                   PlayerFirstName = sl.Player.NameFirst,
                                   PlayerLastName = sl.Player.NameLast,
                                   TeamId = sl.GameParticipant.TeamYear.TeamId,
                                   TeamName = sl.GameParticipant.TeamYear.FullName
                               })
                .Select(slg => new LeagueIndividualStatisticsRowModel
                               {
                                   Year = year,
                                   Player = new StatisticsPlayerInfoModel
                                            {
                                                PlayerId = slg.Key.PlayerId,
                                                PlayerFirstName = slg.Key.PlayerFirstName,
                                                PlayerLastName = slg.Key.PlayerLastName
                                            },
                                   Team = new StatisticsTeamInfoModel
                                          {
                                              TeamId = slg.Key.TeamId,
                                              TeamName = slg.Key.TeamName
                                          },
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
                                   Strikeouts = slg.Sum(sl => sl.StatStrikeouts)
                               });

            return Json(data);
        }

        #endregion

        #region League Team

        [Route("League/Team/{year:years?}")]
        public ActionResult StatisticsForLeagueTeam(YearEnum year = (YearEnum)Consts.CurrentYear)
        {
            var model = new LeagueStatisticsPageModel
            {
                Year = year
            };

            return View(model);
        }

        [HttpPost]
        [Route("LeagueData/Team/{year:years}")]
        public ActionResult LeagueTeamData(YearEnum year)
        {
            var statLines = DbContext.StatLines.AsQueryable();

            if (year != YearEnum.All)
            {
                statLines = statLines.Where(sl => sl.GameParticipant.Game.GameDate.Year == (int)year);
            }

            var data = statLines
                .GroupBy(sl => new
                               {
                                   TeamId = sl.GameParticipant.TeamYear.TeamId,
                                   TeamName = sl.GameParticipant.TeamYear.FullName,
                                   Games = sl.GameParticipant.TeamYear.GameParticipants.Count(gp => gp.StatLines.Any())
                               })
                .Select(slg => new LeagueTeamStatisticsRowModel
                               {
                                   Year = year,
                                   Team = new StatisticsTeamInfoModel
                                          {
                                              TeamId = slg.Key.TeamId,
                                              TeamName = slg.Key.TeamName
                                          },
                                   Games = slg.Key.Games,
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
                                   Strikeouts = slg.Sum(sl => sl.StatStrikeouts)
                               });

            return Json(data);
        }

        #endregion

        #region Player

        [Route("Player/{playerId}/{year:years?}")]
        public ActionResult StatisticsForPlayer(int playerId, YearEnum year = (YearEnum)Consts.CurrentYear)
        {
            var player = DbContext.Players.FirstOrDefault(ty => ty.PlayerId == playerId);
            if (player == null)
            {
                return HttpNotFound();
            }

            var model = new PlayerStatisticsPageModel
                        {
                            Year = year,
                            Player = new StatisticsPlayerInfoModel
                                     {
                                         PlayerId = player.PlayerId,
                                         PlayerFirstName = player.NameFirst,
                                         PlayerLastName = player.NameLast
                                     }
                        };

            return View(model);
        }

        [HttpPost]
        [Route("PlayerData/{playerId}/{year:years}")]
        public ActionResult PlayerData(int playerId, YearEnum year)
        {
            var player = DbContext.Players.FirstOrDefault(ty => ty.PlayerId == playerId);
            if (player == null)
            {
                return HttpNotFound();
            }

            var data = year == YearEnum.All
                           ? GetPlayerCareerData(player)
                           : GetPlayerSeasonData(player, (int)year);

            return Json(data);
        }

        private static IEnumerable<object> GetPlayerCareerData(Player player)
        {
            return player.StatLines
                         .GroupBy(sl => sl.GameParticipant.Game.GameDate.Year)
                         .Select(slg => new PlayerCareerStatisticsRowModel
                                        {
                                            Year = (YearEnum)slg.Key,
                                            Player = new StatisticsPlayerInfoModel
                                                     {
                                                         PlayerId = player.PlayerId
                                                     },
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
                                            Strikeouts = slg.Sum(sl => sl.StatStrikeouts)
                                        });
        }

        private static IEnumerable<object> GetPlayerSeasonData(Player player, int year)
        {
            return from sl in player.StatLines
                   where sl.GameParticipant.Game.GameDate.Year == year
                   orderby sl.GameParticipant.Game.GameDate
                   let opponent = sl.GameParticipant
                                    .Game
                                    .GameParticipants
                                    .Single(gp => gp.GameParticipantId != sl.GameParticipantId)
                                    .TeamYear
                   select new PlayerSeasonStatisticsRowModel
                          {
                              GameId = sl.GameParticipant.GameId,
                              GameDate = sl.GameParticipant.Game.GameDate.ToString(Consts.DateFormat),
                              Year = (YearEnum)year,
                              Opponent = new StatisticsTeamInfoModel
                                     {
                                         TeamId = opponent.TeamId,
                                         TeamName = opponent.FullName
                                     },
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
                              Strikeouts = sl.StatStrikeouts
                          };
        }

        #endregion

        #region Team

        [Route("Team/{teamId}/{year:years?}")]
        public ActionResult StatisticsForTeam(int teamId, YearEnum year = (YearEnum)Consts.CurrentYear, string sort = null)
        {
            var teamYear = DbContext.TeamYears
                                    .OrderByDescending(ty => ty.Year)
                                    .FirstOrDefault(ty => ty.TeamId == teamId && (year == YearEnum.All || ty.Year == (int)year));
            if (teamYear == null)
            {
                return HttpNotFound();
            }

            var model = new TeamStatisticsPageModel
                        {
                            Year = year,
                            Team = new StatisticsTeamInfoModel
                                   {
                                       TeamId = teamYear.TeamId,
                                       TeamName = teamYear.FullName
                                   },
                            SortColumn = sort
                        };

            return View(model);
        }

        [HttpPost]
        [Route("TeamData/{teamId}/{year:years}")]
        public ActionResult TeamData(int teamId, YearEnum year)
        {
            IEnumerable<GameParticipant> gameParticipants;
            if (year == YearEnum.All)
            {
                var team = DbContext.Teams.SingleOrDefault(ty => ty.TeamId == teamId);
                if (team == null)
                {
                    return HttpNotFound();
                }

                gameParticipants = team.TeamYears.SelectMany(ty => ty.GameParticipants);
            }
            else
            {
                var teamYear = DbContext.TeamYears.SingleOrDefault(ty => ty.TeamId == teamId && ty.Year == (int)year);
                if (teamYear == null)
                {
                    return HttpNotFound();
                }

                gameParticipants = teamYear.GameParticipants;
            }

            var data = gameParticipants
                .SelectMany(gp => gp.StatLines)
                .GroupBy(sl => new {sl.PlayerId, sl.Player.NameFirst, sl.Player.NameLast})
                .Select(slg => new TeamPlayerStatisticsRowModel
                               {
                                   Year = year,
                                   Player = new StatisticsPlayerInfoModel
                                            {
                                                PlayerId = slg.Key.PlayerId,
                                                PlayerFirstName = slg.Key.NameFirst,
                                                PlayerLastName = slg.Key.NameLast
                                            },
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
                                   Strikeouts = slg.Sum(sl => sl.StatStrikeouts)
                               });

            return Json(data);
        }

        #endregion
    }
}