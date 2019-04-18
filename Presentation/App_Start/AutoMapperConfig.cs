using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Tcbcsl.Data;
using Tcbcsl.Data.Entities;
using Tcbcsl.Data.Identity;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;
using Tcbcsl.Presentation.Models;

// ReSharper disable UnusedMethodReturnValue.Local

namespace Tcbcsl.Presentation
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(Configure);
            Mapper.AssertConfigurationIsValid();
        }

        private static void Configure(IMapperConfigurationExpression config)
        {
            config.AddGlobalIgnore("ItemSelectList");
            config.AddGlobalIgnore("UrlFor");
            config.AddGlobalIgnore("UrlsFor");

            #region Base mappings

            config.CreateMap<EntityModifiable, AuditDetailsModel>()
                  .ForMember(m => m.CreatedBy, exp => exp.MapFrom(e => e.CreatedByUser.FullName))
                  .ForMember(m => m.ModifiedBy, exp => exp.MapFrom(e => e.ModifiedByUser.FullName));

            config.CreateMap<DateTime, DateTimeOffset>()
                  .ConvertUsing(dateTime => new DateTimeOffset(dateTime, CentralTimeZone.DaylightSavingsOffset));

            config.CreateMap<DateTime?, DateTimeOffset?>()
                  .ConvertUsing(dateTime => dateTime == null ? null : (DateTimeOffset?)Mapper.Map<DateTimeOffset>(dateTime.Value));

            config.CreateMap<DateTimeOffset, DateTime>()
                  .ConvertUsing(dateTimeOffset => dateTimeOffset.DateTime);

            config.CreateMap<DateTimeOffset?, DateTime?>()
                  .ConvertUsing(dateTimeOffset => dateTimeOffset == null ? null : (DateTime?)Mapper.Map<DateTime>(dateTimeOffset.Value));

            #endregion

            #region Church

            config.CreateMap<Church, ChurchEditModel>()
                  .MapEditModelBaseWithContactInfo();

            config.CreateMap<ChurchEditModel, Church>()
                  .MapIgnoreEntityWithContactInfo()
                  .ForMember(m => m.TeamYears, exp => exp.Ignore());

            #endregion

            #region Coach

            config.CreateMap<Coach, CoachEditModel>()
                  .MapEditModelBaseWithContactInfo();

            config.CreateMap<CoachEditModel, Coach>()
                  .MapIgnoreEntityWithContactInfo()
                  .ForMember(m => m.TeamYears, exp => exp.Ignore());

            #endregion

            #region Contact Info mappings

            config.CreateMap<Address, AddressEditModel>()
                  .MapEditModelBaseWithAudit()
                  .ForMember(m => m.State, exp => exp.MapFrom(e => Mapper.Map<StateEditModel>(e.State) ?? new StateEditModel()));

            config.CreateMap<Address, AddressInfoModel>()
                  .ForMember(m => m.State, exp => exp.MapFrom(e => e.State.Abbreviation));

            config.CreateMap<AddressEditModel, Address>()
                  .MapIgnoreEntityModifiable()
                  .ForMember(e => e.AddressId, exp => exp.Ignore())
                  .ForMember(e => e.StateId, exp => exp.MapFrom(m => m.State.StateId))
                  .ForMember(e => e.State, exp => exp.Ignore());

            config.CreateMap<ICollection<ContactPhoneNumber>, PhoneEditModelList>()
                  .ConvertUsing(e => new PhoneEditModelList(Mapper.Map<List<PhoneEditModel>>(e)));

            config.CreateMap<ContactPhoneNumber, PhoneEditModel>()
                  .MapEditModelBaseWithAudit()
                  .ForMember(m => m.PhoneTypeName, exp => exp.MapFrom(e => e.PhoneNumberType.Description))
                  .ForMember(m => m.PhoneTypes, exp => exp.Ignore());

            config.CreateMap<ContactPhoneNumber, string>()
                  .ConvertUsing(e => $"{e.PhoneNumber} ({e.PhoneNumberType.Description})");

            config.CreateMap<EntityWithContactInfo, ContactInfoModel>()
                  .ForMember(m => m.EmailAddress, exp => exp.MapFrom(e => MvcHtmlString.Create(e.EmailAddress.Replace("@", "@<span style=\"display: none;\">null</span>"))));

            config.CreateMap<PhoneEditModel, ContactPhoneNumber>()
                  .MapIgnoreEntityModifiable()
                  .ForMember(e => e.PhoneNumberType, exp => exp.Ignore())
                  .ForMember(e => e.ChurchId, exp => exp.Ignore())
                  .ForMember(e => e.Church, exp => exp.Ignore())
                  .ForMember(e => e.CoachId, exp => exp.Ignore())
                  .ForMember(e => e.Coach, exp => exp.Ignore());

            config.CreateMap<PhoneNumberType, PhoneTypeModel>();

            config.CreateMap<State, StateEditModel>()
                  .ForMember(m => m.StateName, exp => exp.MapFrom(e => e.Name));

            #endregion

            #region Game

            config.CreateMap<Game, GameEditModel>()
                  .MapEditModelBaseWithAudit();

            config.CreateMap<GameParticipant, GameParticipantEditModel>()
                  .MapEditModelBaseWithAudit();

            config.CreateMap<GameStatus, GameEditStatusModel>();
            config.CreateMap<GameStatus, string>()
                  .ConvertUsing(e => e?.Description);

            config.CreateMap<GameType, GameEditTypeModel>();

            config.CreateMap<TeamYear, GameEditTeamModel>();

            config.CreateMap<GameEditModel, Game>()
                  .MapIgnoreEntityModifiable()
                  .ForMember(e => e.GameId, exp => exp.Ignore())
                  .ForMember(e => e.GameTypeId, exp => exp.MapFrom(m => m.GameType.GameTypeId))
                  .ForMember(e => e.GameType, exp => exp.Ignore())
                  .ForMember(e => e.GameStatusId, exp => exp.MapFrom(m => m.GameStatus.GameStatusId))
                  .ForMember(e => e.GameStatus, exp => exp.Ignore())
                  .ForMember(e => e.GameParticipants, exp => exp.Ignore())
                  .ForMember(e => e.GameResultReports, exp => exp.Ignore())
                  .ForMember(e => e.IsFinalized, exp => exp.MapFrom(m => m.GameStatus.GameStatusId != GameStatus.Scheduled ||
                                                                         m.HomeParticipant.RunsScored != 0 ||
                                                                         m.RoadParticipant.RunsScored != 0));

            config.CreateMap<GameParticipantEditModel, GameParticipant>()
                  .MapIgnoreEntityModifiable()
                  .ForMember(e => e.GameParticipantId, exp => exp.Ignore())
                  .ForMember(e => e.GameId, exp => exp.Ignore())
                  .ForMember(e => e.Game, exp => exp.Ignore())
                  .ForMember(e => e.IsHost, exp => exp.Ignore())
                  .ForMember(e => e.TeamYearId, exp => exp.MapFrom(m => m.TeamYear.TeamYearId))
                  .ForMember(e => e.TeamYear, exp => exp.Ignore())
                  .ForMember(e => e.StatLines, exp => exp.Ignore());

            #endregion

            #region Game Results

            config.CreateMap<TeamYear, GameResultsListModel>()
                  .ForMember(m => m.Team, exp => exp.MapFrom(e => e))
                  .ForMember(m => m.GameParticipantId, exp => exp.Ignore())
                  .ForMember(m => m.GameDate, exp => exp.Ignore())
                  .ForMember(m => m.Opponent, exp => exp.Ignore())
                  .ForMember(m => m.Outcome, exp => exp.Ignore())
                  .ForMember(m => m.IsWaitingForMyInput, exp => exp.Ignore())
                  .ForMember(m => m.IsFinalized, exp => exp.Ignore())
                  .ForMember(m => m.NoStats, exp => exp.Ignore());

            config.CreateMap<GameParticipant, GameResultsListModel>()
                  .ForMember(m => m.Team, exp => exp.MapFrom(e => e.TeamYear))
                  .ForMember(m => m.GameDate, exp => exp.MapFrom(e => e.Game.GameDate))
                  .ForMember(m => m.Opponent, exp => exp.MapFrom(e => e.Opponent.TeamYear.FullName))
                  .ForMember(m => m.Outcome, exp => exp.MapFrom(e => e.GetOutcome()))
                  .ForMember(m => m.KeepsStats, exp => exp.Ignore())
                  .ForMember(m => m.IsWaitingForMyInput, exp => exp.MapFrom(e => e.Game.GetIsWaitingForMyInput()))
                  .ForMember(m => m.IsFinalized, exp => exp.MapFrom(e => e.Game.IsFinalized))
                  .ForMember(m => m.NoStats, exp => exp.MapFrom(e => !e.StatLines.Any()));

            config.CreateMap<Game, GameResultsEditModel>()
                  .ForMember(m => m.RoadTeam, exp => exp.MapFrom(e => e.RoadParticipant.TeamYear))
                  .ForMember(m => m.HomeTeam, exp => exp.MapFrom(e => e.HomeParticipant.TeamYear))
                  .ForMember(m => m.ResultReports, exp => exp.MapFrom(e => e.GameResultReports))
                  .ForMember(m => m.NewReport, exp => exp.MapFrom(e => e));

            config.CreateMap<Game, GameResultsEditCreateReportModel>()
                  .ForMember(m => m.CurrentResult, exp => exp.MapFrom(g => g.GetResultDescription()))
                  .ForMember(m => m.IsConfirmable, exp => exp.MapFrom(g => g.GetIsWaitingForMyInput() && g.GameResultReports.Any(r => r.GameStatusId != GameStatus.Scheduled)))
                  .ForMember(m => m.Team, exp => exp.UseValue(new GameResultsTeamModel()))
                  .ForMember(m => m.IsConfirmation, exp => exp.Ignore())
                  .ForMember(m => m.Note, exp => exp.Ignore());

            config.CreateMap<GameStatus, GameResultsStatusModel>();

            config.CreateMap<GameResultReport, GameResultsEditReportModel>()
                  .ForMember(m => m.UserFullName, exp => exp.MapFrom(e => e.CreatedByUser.FullName))
                  .ForMember(m => m.ReportDate, exp => exp.MapFrom(e => e.Created))
                  .ForMember(m => m.SubmittedFrom, exp => exp.MapFrom(e => e.TeamId == e.Game.HomeParticipant.TeamYear.TeamId
                                                                               ? ReportSubmitter.HomeTeam
                                                                               : e.TeamId == e.Game.RoadParticipant.TeamYear.TeamId
                                                                                     ? ReportSubmitter.RoadTeam
                                                                                     : ReportSubmitter.League));

            config.CreateMap<GameResultsEditCreateReportModel, GameResultReport>()
                  .ForMember(m => m.GameResultReportId, exp => exp.Ignore())
                  .ForMember(m => m.GameId, exp => exp.Ignore())
                  .ForMember(m => m.Game, exp => exp.Ignore())
                  .ForMember(m => m.TeamId, exp => exp.MapFrom(e => e.Team.TeamId))
                  .ForMember(m => m.Team, exp => exp.Ignore())
                  .ForMember(m => m.GameStatusId, exp => exp.MapFrom(e => e.GameStatus.GameStatusId))
                  .ForMember(m => m.GameStatus, exp => exp.Ignore())
                  .ForMember(m => m.RoadTeamScore, exp => exp.MapFrom(e => e.RoadParticipant.RunsScored))
                  .ForMember(m => m.HomeTeamScore, exp => exp.MapFrom(e => e.HomeParticipant.RunsScored))
                  .MapIgnoreEntityCreatable();

            #endregion

            #region NewsItem

            config.CreateMap<NewsItem, NewsEditTeamModel>()
                  .ForMember(m => m.IsReadonly, exp => exp.MapFrom(e => CentralTimeZone.Now - e.Created > TimeSpan.FromDays(30)))
                  .ForMember(m => m.FullName, exp => exp.MapFrom(e => e.GetTeamName() ?? Consts.LeagueNameForList))
                  .ForMember(m => m.Teams, exp => exp.MapFrom(e => new List<TeamBasicInfoModel>()));

            config.CreateMap<NewsItem, NewsEditModel>()
                  .MapEditModelBaseWithAudit()
                  .ForMember(m => m.Team, exp => exp.MapFrom(e => e));

            config.CreateMap<NewsEditModel, NewsItem>()
                  .MapIgnoreEntityModifiable()
                  .ForMember(e => e.TeamId, exp => exp.MapFrom(m => m.Team.TeamId))
                  .ForMember(e => e.Team, exp => exp.Ignore())
                  .ForMember(e => e.Content, exp => exp.MapFrom(m => m.Content.Sanitize()));

            #endregion

            #region PageContent

            config.CreateMap<PageContent, PageContentEditModel>()
                  .MapEditModelBaseWithAudit();

            config.CreateMap<PageContentEditModel, PageContent>()
                  .MapIgnoreEntityModifiable()
                  .ForMember(e => e.Content, exp => exp.MapFrom(m => m.Content.Sanitize()));

            #endregion

            #region Player

            config.CreateMap<Player, PlayerEditModel>()
                  .MapEditModelBaseWithAudit()
                  .ForMember(m => m.Team, exp => exp.MapFrom(e => e.CurrentTeam))
                  .ForMember(m => m.HasStatsFor, exp => exp.MapFrom(e => e.StatLines
                                                                          .Select(sl => sl.GameParticipant.TeamYear)
                                                                          .Distinct()
                                                                          .GroupBy(ty => ty.Team)));

            config.CreateMap<Team, PlayerEditTeamModel>()
                  .ForMember(m => m.FullName, exp => exp.MapFrom(e => e.TeamYears.OrderByDescending(ty => ty.Year).First().FullName));

            config.CreateMap<IGrouping<Team, TeamYear>, string>()
                  .ConvertUsing(g => $"{g.Key.TeamYears.OrderByDescending(ty => ty.Year).First().FullName} ({string.Join(", ", g.Select(ty => ty.Year).OrderByDescending(y => y))})");

            config.CreateMap<PlayerEditModel, Player>()
                  .MapIgnoreEntityModifiable()
                  .ForMember(e => e.PlayerId, exp => exp.Ignore())
                  .ForMember(e => e.CurrentTeamId, exp => exp.MapFrom(m => m.Team.TeamId))
                  .ForMember(e => e.CurrentTeam, exp => exp.Ignore())
                  .ForMember(e => e.StatLines, exp => exp.Ignore());

            #endregion

            #region Schedule

            config.CreateMap<KeyValuePair<GameBucket, List<Game>>, ScheduleBucketEditModel>()
                  .ForMember(m => m.Bucket, exp => exp.MapFrom(kvp => kvp.Key))
                  .ForMember(m => m.Games, exp => exp.MapFrom(kvp => kvp.Value));

            config.CreateMap<IGrouping<GameBucket, Game>, ScheduleBucketEditModel>()
                  .ForMember(m => m.Bucket, exp => exp.MapFrom(g => g.Key))
                  .ForMember(m => m.Games, exp => exp.MapFrom(g => g.ToList()));

            config.CreateMap<Game, ScheduleGameEditModel>()
                  .ForMember(m => m.Entered, exp => exp.MapFrom(e => e.GameStatusId != GameStatus.Scheduled))
                  .ForMember(m => m.Outcome, exp => exp.MapFrom(e => e.GameStatus.Description));

            config.CreateMap<GameParticipant, ScheduleGameParticipantEditModel>()
                  .ForMember(m => m.TeamName, exp => exp.MapFrom(e => e.TeamYear.FullName));

            #endregion

            #region Statistics

            config.CreateMap<GameParticipant, StatisticsEditModel>()
                  .ForMember(m => m.TeamName, exp => exp.MapFrom(e => e.TeamYear.FullName))
                  .ForMember(m => m.GameDate, exp => exp.MapFrom(e => e.Game.GameDate))
                  .ForMember(m => m.StatLines, exp => exp.MapFrom(e => e.StatLines.OrderBy(sl => sl.BattingOrderPosition)));

            config.CreateMap<StatLine, StatisticsEditStatLineModel>()
                  .MapEditModelBaseWithAudit();

            config.CreateMap<StatisticsEditStatLineModel, StatLine>()
                  .ForMember(e => e.PlayerId, exp => exp.MapFrom(m => m.Player.PlayerId))
                  .ForMember(e => e.Player, exp => exp.Ignore())
                  .ForMember(e => e.GameParticipantId, exp => exp.Ignore())
                  .ForMember(e => e.GameParticipant, exp => exp.Ignore())
                  .MapIgnoreEntityModifiable()
                  .IgnoreAllPropertiesWithAnInaccessibleSetter();

            config.CreateMap<Player, StatisticsEditPlayerModel>();

            #endregion

            #region Team

            config.CreateMap<TeamYear, TeamBasicInfoModel>();

            config.CreateMap<TeamYear, TeamPickerModel>()
                  .ForMember(m => m.Teams, exp => exp.Ignore())
                  .ForMember(m => m.Years, exp => exp.MapFrom(e => e.Team
                                                                    .TeamYears
                                                                    .Select(ty => ty.Year)
                                                                    .OrderByDescending(y => y)
                                                                    .ToArray()));

            config.CreateMap<TeamYear, TeamEditModel>()
                  .MapEditModelBaseWithAudit()
                  .ForMember(m => m.Conference, exp => exp.MapFrom(e => e.DivisionYear.ConferenceYear))
                  .ForMember(m => m.Division, exp => exp.MapFrom(e => e.DivisionYear))
                  .ForMember(m => m.Clinch, exp => exp.MapFrom(e => e.Clinch == null ? new TeamEditClinchModel() : Mapper.Map<TeamEditClinchModel>(e.Clinch)))
                  .ForMember(m => m.FieldInformation, exp => exp.MapFrom(e => e.Team.FieldInformation))
                  .ForMember(m => m.Comments, exp => exp.MapFrom(e => e.Team.Comments))
                  .ForMember(m => m.YearModel, exp => exp.MapFrom(e => new YearModel {Year = e.Year}));

            config.CreateMap<TeamYear, TeamYearTransferModel>()
                  .ForMember(m => m.ExistsInCurrentYear, exp => exp.Ignore())
                  .ForMember(m => m.Conference, exp => exp.MapFrom(e => e.DivisionYear.ConferenceYear))
                  .ForMember(m => m.Division, exp => exp.MapFrom(e => e.DivisionYear))
                  .ForMember(m => m.YearModel, exp => exp.MapFrom(e => new YearModel {Year = e.Year}));

            config.CreateMap<TeamYear, TeamManageModel>()
                  .ForMember(m => m.Team, exp => exp.MapFrom(e => e));

            config.CreateMap<ConferenceYear, TeamEditDivisionModel>()
                  .ForMember(m => m.DivisionYearId, exp => exp.Ignore());

            config.CreateMap<DivisionYear, TeamEditDivisionModel>();

            config.CreateMap<Church, TeamEditChurchModel>();

            config.CreateMap<Coach, TeamEditCoachModel>()
                  .ForMember(m => m.SortableName, exp => exp.MapFrom(e => $"{e.LastName}, {e.FirstName}"));

            config.CreateMap<string, TeamEditClinchModel>()
                  .ForMember(m => m.ClinchChar, exp => exp.MapFrom(s => s))
                  .ForMember(m => m.Description, exp => exp.MapFrom(s => Consts.ClinchDescriptions[s]));

            config.CreateMap<TeamEditModel, TeamYear>()
                  .MapIgnoreEntityModifiable()
                  .ForMember(e => e.TeamId, exp => exp.Ignore())
                  .ForMember(e => e.Year, exp => exp.Ignore())
                  .ForMember(e => e.TeamYearId, exp => exp.Ignore())
                  .ForMember(e => e.Team, exp => exp.Ignore())
                  .ForMember(e => e.FullName, exp => exp.Ignore())
                  .ForMember(e => e.ChurchId, exp => exp.MapFrom(m => m.Church.ChurchId))
                  .ForMember(e => e.Church, exp => exp.Ignore())
                  .ForMember(e => e.DivisionYearId, exp => exp.MapFrom(m => m.Division.DivisionYearId))
                  .ForMember(e => e.DivisionYear, exp => exp.Ignore())
                  .ForMember(e => e.HeadCoachId, exp => exp.MapFrom(m => m.HeadCoach.CoachId))
                  .ForMember(e => e.HeadCoach, exp => exp.Ignore())
                  .ForMember(e => e.Clinch, exp => exp.MapFrom(m => m.Clinch.ClinchChar))
                  .ForMember(e => e.GameParticipants, exp => exp.Ignore());

            config.CreateMap<TeamEditModel, Team>()
                  .MapIgnoreEntityModifiable()
                  .ForMember(e => e.TeamId, exp => exp.Ignore())
                  .ForMember(e => e.TeamYears, exp => exp.Ignore())
                  .ForMember(e => e.NewsItems, exp => exp.Ignore())
                  .ForMember(e => e.Players, exp => exp.Ignore())
                  .ForMember(e => e.ManagingUsers, exp => exp.Ignore())
                  .ForMember(e => e.GameResultReports, exp => exp.Ignore());

            config.CreateMap<TeamListEditModel, TeamYear>()
                  .MapIgnoreEntityModifiable()
                  .ForMember(e => e.TeamId, exp => exp.Ignore())
                  .ForMember(e => e.Year, exp => exp.Ignore())
                  .ForMember(e => e.TeamYearId, exp => exp.Ignore())
                  .ForMember(e => e.Team, exp => exp.Ignore())
                  .ForMember(e => e.TeamName, exp => exp.Ignore())
                  .ForMember(e => e.FullName, exp => exp.Ignore())
                  .ForMember(e => e.ChurchId, exp => exp.Ignore())
                  .ForMember(e => e.Church, exp => exp.Ignore())
                  .ForMember(e => e.DivisionYear, exp => exp.Ignore())
                  .ForMember(e => e.HeadCoachId, exp => exp.Ignore())
                  .ForMember(e => e.HeadCoach, exp => exp.Ignore())
                  .ForMember(e => e.GameParticipants, exp => exp.Ignore());

            #endregion

            #region User Management

            config.CreateMap<TcbcslUser, UserEditModel>()
                  .ForMember(m => m.Roles, exp => exp.MapFrom(e => new RolesEditModel {RoleIds = e.Roles.Select(r => r.RoleId).ToList()}))
                  .ForMember(m => m.AssignedTeams, exp => exp.MapFrom(e => new AssignedTeamsEditModel
                                                                           {
                                                                               TeamIds = e.AssignedTeams.Select(r => r.TeamId).ToList(),
                                                                               SelectedTeamNames =
                                                                                   string.Join(", ", e.AssignedTeams.Select(t => Mapper.Map<string>(t.TeamYears.Last())))
                                                                           }));

            config.CreateMap<IEnumerable<IdentityRole>, string>()
                  .ConvertUsing(en => string.Join(", ", en.Select(r => r.Name)));

            config.CreateMap<IdentityRole, SelectListItem>()
                  .ForMember(m => m.Value, exp => exp.MapFrom(e => e.Id))
                  .ForMember(m => m.Text, exp => exp.MapFrom(e => e.Name))
                  .ForMember(m => m.Disabled, exp => exp.Ignore())
                  .ForMember(m => m.Group, exp => exp.Ignore())
                  .ForMember(m => m.Selected, exp => exp.Ignore());

            config.CreateMap<Team, SelectListItem>()
                  .ForMember(m => m.Value, exp => exp.MapFrom(e => e.TeamId))
                  .ForMember(m => m.Text, exp => exp.MapFrom(e => e.TeamYears.OrderByDescending(ty => ty.Year).First()))
                  .ForMember(m => m.Disabled, exp => exp.Ignore())
                  .ForMember(m => m.Group, exp => exp.Ignore())
                  .ForMember(m => m.Selected, exp => exp.Ignore());

            config.CreateMap<TeamYear, string>()
                  .ConvertUsing(ty => $"{ty.FullName} ({ty.Year})");

            config.CreateMap<UserEditModel, TcbcslUser>()
                  .ForMember(m => m.FirstName, exp => exp.MapFrom(e => e.FirstName))
                  .ForMember(m => m.LastName, exp => exp.MapFrom(e => e.LastName))
                  .ForMember(e => e.Roles, exp => exp.Ignore())
                  .ForMember(e => e.AssignedTeams, exp => exp.Ignore())
                  .ForMember(e => e.UserName, exp => exp.Ignore())
                  .ForMember(e => e.EmailConfirmed, exp => exp.Ignore())
                  .ForMember(e => e.PasswordHash, exp => exp.Ignore())
                  .ForMember(e => e.SecurityStamp, exp => exp.Ignore())
                  .ForMember(e => e.PhoneNumber, exp => exp.Ignore())
                  .ForMember(e => e.PhoneNumberConfirmed, exp => exp.Ignore())
                  .ForMember(e => e.TwoFactorEnabled, exp => exp.Ignore())
                  .ForMember(e => e.LockoutEndDateUtc, exp => exp.Ignore())
                  .ForMember(e => e.LockoutEnabled, exp => exp.Ignore())
                  .ForMember(e => e.AccessFailedCount, exp => exp.Ignore())
                  .ForMember(e => e.Claims, exp => exp.Ignore())
                  .ForMember(e => e.Logins, exp => exp.Ignore());

            #endregion
        }

        #region Mapping Extension Methods

        private static bool GetIsWaitingForMyInput(this Game game)
        {
            if (game.IsFinalized)
            {
                return false;
            }

            foreach (var report in game.GameResultReports.Where(r => r.GameStatusId != GameStatus.Scheduled).OrderByDescending(r => r.Created))
            {
                if (report.TeamId.HasValue && UserCache.AssignedTeams.ContainsKey(report.TeamId.Value))
                {
                    return false;
                }

                if (!report.IsConfirmation)
                {
                    return true;
                }
            }

            return game.GameDate < CentralTimeZone.Now;
        }

        private static string GetResultDescription(this Game g)
        {
            switch (g.GameStatusId)
            {
                case GameStatus.Scheduled:
                    return "";
                case GameStatus.Postponed:
                    return "Postponed";
                case GameStatus.RainedOut:
                    return "Rained Out";
            }

            var participants = (from gp in g.GameParticipants
                                orderby gp.RunsScored descending
                                select new
                                       {
                                           Team = gp.TeamYear.FullName,
                                           Runs = gp.RunsScored
                                       }).ToList();

            return g.GameStatusId == GameStatus.Forfeited
                       ? $"{participants[1].Team} forfeit to {participants[0].Team}"
                       : $"{participants[0].Team} {participants[0].Runs}, {participants[1].Team} {participants[1].Runs}";
        }

        private static string GetOutcome(this GameParticipant gp)
        {
            var won = gp.RunsScored > gp.Opponent.RunsScored;
            var lost = gp.RunsScored < gp.Opponent.RunsScored;

            return gp.Game.GameStatus.DisplayOutcome
                       ? (gp.Game.GameStatus.AllowStatistics
                              ? (won ? "W" : lost ? "L" : "T") + " " + gp.RunsScored + "-" + gp.Opponent.RunsScored
                              : gp.Game.GameStatus.Description)
                         + (gp.Game.IsFinalized
                                ? ""
                                : "*")
                       : string.Empty;
        }

        private static string GetTeamName(this NewsItem newsItem)
        {
            return newsItem.Team?
                           .TeamYears
                           .SingleOrDefault(ty => ty.Year == newsItem.StartDate.Year)?
                           .FullName;
        }

        private static IMappingExpression<TEntity, TModel> MapEditModelBaseWithAudit<TEntity, TModel>(this IMappingExpression<TEntity, TModel> mapping)
            where TEntity : EntityModifiable
            where TModel : EditModelBaseWithAudit
        {
            return mapping.ForMember(m => m.AuditDetails, exp => exp.MapFrom(e => Mapper.Map<AuditDetailsModel>(e)));
        }

        private static IMappingExpression<TEntity, TModel> MapEditModelBaseWithContactInfo<TEntity, TModel>(this IMappingExpression<TEntity, TModel> mapping)
            where TEntity : EntityWithContactInfo
            where TModel : EditModelBaseWithContactInfo
        {
            return mapping.MapEditModelBaseWithAudit()
                          .ForMember(m => m.Address, exp => exp.MapFrom(e => Mapper.Map<AddressEditModel>(e.Address) ?? new AddressEditModel {State = new StateEditModel()}));
        }

        private static IMappingExpression<TModel, TEntity> MapIgnoreEntityCreatable<TModel, TEntity>(this IMappingExpression<TModel, TEntity> mapping)
            where TEntity : EntityCreatable
        {
            return mapping.ForMember(e => e.Created, exp => exp.Ignore())
                          .ForMember(e => e.CreatedBy, exp => exp.Ignore())
                          .ForMember(e => e.CreatedByUser, exp => exp.Ignore());
        }

        private static IMappingExpression<TModel, TEntity> MapIgnoreEntityModifiable<TModel, TEntity>(this IMappingExpression<TModel, TEntity> mapping)
            where TEntity : EntityModifiable
        {
            return mapping.MapIgnoreEntityCreatable()
                          .ForMember(e => e.Modified, exp => exp.Ignore())
                          .ForMember(e => e.ModifiedBy, exp => exp.Ignore())
                          .ForMember(e => e.ModifiedByUser, exp => exp.Ignore());
        }

        private static IMappingExpression<TModel, TEntity> MapIgnoreEntityWithContactInfo<TModel, TEntity>(this IMappingExpression<TModel, TEntity> mapping)
            where TEntity : EntityWithContactInfo
        {
            return mapping.MapIgnoreEntityModifiable()
                          .ForMember(e => e.AddressId, exp => exp.Ignore())
                          .ForMember(e => e.PhoneNumbers, exp => exp.Ignore());
        }

        #endregion
    }
}