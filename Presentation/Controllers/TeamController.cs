﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tcbcsl.Presentation.Models;
using Tcbcsl.Presentation.Services;

namespace Tcbcsl.Presentation.Controllers
{
    public class TeamController : ControllerBase
    {
        #region Constructor and Private Fields

        private static readonly List<StatsCategory> StatsCategories = new List<StatsCategory>
                                                                      {
                                                                          new StatsCategory {Name = "Batting Average (AVG)",     Field = "AVG", IsPercentage = true},
                                                                          new StatsCategory {Name = "Home Runs (HR)",            Field = "HR"},
                                                                          new StatsCategory {Name = "Walks (BB)",                Field = "BB"},
                                                                          new StatsCategory {Name = "Runs Batted In (RBI)",      Field = "RBI"},
                                                                          new StatsCategory {Name = "Runs (R)",                  Field = "R"},
                                                                          new StatsCategory {Name = "On-base + Slugging (OPS)",  Field = "OPS", IsPercentage = true}
                                                                      };

        private readonly ScheduleService _scheduleService;

        public TeamController()
        {
            _scheduleService = new ScheduleService(DbContext);
        }

        #endregion

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
                                            .OrderBy(ty => ty.FullName)
                                            .Select(ty => new TeamsListTeamModel
                                                          {
                                                              TeamId = ty.TeamId,
                                                              Year = year,
                                                              TeamName = ty.FullName
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
            var teamYear = DbContext
                .TeamYears
                .SingleOrDefault(ty => ty.TeamId == teamId && ty.Year == year);

            if (teamYear == null)
            {
                return HttpNotFound($"Team {teamId} not found for {year}.");
            }

            var model = new TeamViewModel
                        {
                            Year = teamYear.Year,
                            Years = teamYear.Team.TeamYears.Select(ty => ty.Year).OrderByDescending(y => y).ToArray(),
                            TeamId = teamYear.TeamId,
                            TeamName = teamYear.FullName,
                            DivisionName = teamYear.DivisionYear.Name,
                            ChurchId = teamYear.ChurchId,
                            ChurchName = teamYear.Church.FullName,
                            Coach = new TeamCoachModel
                                    {
                                        CoachId = teamYear.HeadCoachId,
                                        Year = year,
                                        Name = teamYear.HeadCoach.FullName,
                                        Comments = teamYear.HeadCoach.Comments,
                                        ContactInfo = Mapper.Map<ContactInfoModel>(teamYear.HeadCoach)
                                    },
                            Field = teamYear.Team.FieldInformation,
                            Comments = teamYear.Team.Comments,
                            NewsItems = new ContentService(DbContext).GetCurrentNews(teamYear.TeamId),
                            Schedule = new TeamScheduleModel
                                       {
                                           TeamId = teamYear.TeamId,
                                           Year = year,
                                           Games = _scheduleService.GetTeamSchedule(teamYear)
                                       }
                        };

            var teamGamesCount = teamYear.GameParticipants.Count(gp => gp.StatLines.Any());
            if (teamGamesCount > 0)
            {
                model.StatsLeaders = StatsCategories
                    .Select(cat => GetStatsCategoryLeader(cat, model.TeamId, year, (teamGamesCount + 1) / 2))
                    .ToList();
            }

            return View(model);
        }

        [Route("Team/{teamId}/ScheduleDownload")]
        public ActionResult ScheduleDownload(int teamId)
        {
            var teamYear = DbContext
                .TeamYears
                .SingleOrDefault(ty => ty.TeamId == teamId && ty.Year == Consts.CurrentYear);

            if (teamYear == null)
            {
                return HttpNotFound($"Team {teamId} not found for {Consts.CurrentYear}.");
            }

            var model = new TeamScheduleDownloadModel
            {
                Year = teamYear.Year,
                TeamId = teamYear.TeamId,
                TeamName = teamYear.FullName,
                Games = _scheduleService.GetTeamDownloadSchedule(teamYear)
            };

            Response.AddHeader("Content-Disposition", $"attachment; filename=\"{Url.Encode(teamYear.FullName)}-{Consts.CurrentYear}-Calendar.ics\"");
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
                                  Value = category.Field == "AVG"
                                              ? ps.AVG
                                              : category.Field == "HR"
                                                    ? ps.HR
                                                    : category.Field == "BB"
                                                          ? ps.BB
                                                          : category.Field == "RBI"
                                                                ? ps.RBI
                                                                : category.Field == "R"
                                                                      ? ps.Runs
                                                                      : ps.OPS
                              });

            var topValue = selectedValues
                .GroupBy(val => val.Value)
                .OrderByDescending(group => group.Key)
                .FirstOrDefault();

            var leader = new StatsLeaderModel
                         {
                             Year = year,
                             TeamId = teamId,
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
                   let plateAppearances = lineGroup.Sum(line => line.StatPlateAppearances)
                   let atBats = lineGroup.Sum(line => line.StatAtBats)
                   select new PlayerStats
                          {
                              PlayerId = lineGroup.Key.PlayerId,
                              Name = lineGroup.Key.FirstName + " " + lineGroup.Key.LastName,
                              Games = lineGroup.Count(),
                              AVG = atBats == 0 ? 0 : lineGroup.Sum(line => line.StatHits) / (decimal)atBats,
                              HR = lineGroup.Sum(line => line.StatHomeRuns),
                              BB = lineGroup.Sum(line => line.StatWalks),
                              RBI = lineGroup.Sum(line => line.StatRunsBattedIn),
                              Runs = lineGroup.Sum(line => line.StatRuns),
                              OPS = (plateAppearances == 0 ? 0 : lineGroup.Sum(line => line.StatHits + line.StatWalks) / (decimal)plateAppearances) +
                                    (atBats == 0 ? 0 : lineGroup.Sum(line => line.StatTotalBases) / (decimal)atBats)
                          };
        }
    }
}