using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
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
            #region Base mappings

            Mapper.CreateMap<EntityModifiable, AuditDetailsModel>();

            #endregion

            #region Church

            Mapper.CreateMap<Church, ChurchEditModel>()
                  .MapEditModelBaseWithContactInfo();

            Mapper.CreateMap<ChurchEditModel, Church>()
                  .MapEntityWithContactInfo()
                  .ForMember(m => m.TeamYears, exp => exp.Ignore());

            #endregion

            #region Coach

            Mapper.CreateMap<Coach, CoachEditModel>()
                  .MapEditModelBaseWithContactInfo();

            Mapper.CreateMap<CoachEditModel, Coach>()
                  .MapEntityWithContactInfo()
                  .ForMember(m => m.TeamYears, exp => exp.Ignore());

            #endregion

            #region Contact Info mappings

            Mapper.CreateMap<Address, AddressEditModel>()
                  .MapEditModelBaseWithAudit();

            Mapper.CreateMap<Address, AddressInfoModel>()
                  .ForMember(m => m.State, exp => exp.MapFrom(e => e.State.Abbreviation));

            Mapper.CreateMap<AddressEditModel, Address>()
                  .MapEntityModifiable()
                  .ForMember(e => e.StateId, exp => exp.MapFrom(m => m.State.StateId))
                  .ForMember(e => e.State, exp => exp.Ignore());

            Mapper.CreateMap<ICollection<ContactPhoneNumber>, PhoneEditModelList>()
                  .ConvertUsing(e => new PhoneEditModelList(Mapper.Map<List<PhoneEditModel>>(e)));

            Mapper.CreateMap<ContactPhoneNumber, PhoneEditModel>()
                  .MapEditModelBaseWithAudit()
                  .ForMember(m => m.PhoneTypeName, exp => exp.MapFrom(e => e.PhoneNumberType.Description))
                  .ForMember(m => m.PhoneTypes, exp => exp.Ignore());

            Mapper.CreateMap<ContactPhoneNumber, string>()
                  .ConvertUsing(e => $"{e.PhoneNumber} ({e.PhoneNumberType.Description})");

            Mapper.CreateMap<EntityWithContactInfo, ContactInfoModel>()
                  .ForMember(m => m.EmailAddress, exp => exp.MapFrom(e => MvcHtmlString.Create(e.EmailAddress.Replace("@", "@<span style=\"display: none;\">null</span>"))));

            Mapper.CreateMap<PhoneEditModel, ContactPhoneNumber>()
                  .MapEntityModifiable()
                  .ForMember(e => e.PhoneNumberType, exp => exp.Ignore())
                  .ForMember(e => e.ChurchId, exp => exp.Ignore())
                  .ForMember(e => e.Church, exp => exp.Ignore())
                  .ForMember(e => e.CoachId, exp => exp.Ignore())
                  .ForMember(e => e.Coach, exp => exp.Ignore());

            Mapper.CreateMap<PhoneNumberType, PhoneTypeModel>();

            Mapper.CreateMap<State, StateEditModel>()
                  .ForMember(m => m.StateName, exp => exp.MapFrom(e => e.Name))
                  .ForMember(m => m.ItemSelectList, exp => exp.Ignore());

            #endregion

            #region Game

            Mapper.CreateMap<Game, GameEditModel>()
                  .MapEditModelBaseWithAudit()
                  .ForMember(m => m.RoadTeam, exp => exp.MapFrom(e => e.GameParticipants.SingleOrDefault(gp => !gp.IsHost)))
                  .ForMember(m => m.HomeTeam, exp => exp.MapFrom(e => e.GameParticipants.SingleOrDefault(gp => gp.IsHost)))
                  .ForMember(m => m.ReturnUrl, exp => exp.Ignore());

            Mapper.CreateMap<GameParticipant, GameParticipantEditModel>()
                  .MapEditModelBaseWithAudit();

            Mapper.CreateMap<GameStatus, GameEditStatusModel>()
                  .ForMember(m => m.ItemSelectList, exp => exp.Ignore());

            Mapper.CreateMap<GameType, GameEditTypeModel>()
                  .ForMember(m => m.ItemSelectList, exp => exp.Ignore());

            Mapper.CreateMap<TeamYear, GameEditTeamModel>()
                  .ForMember(m => m.ItemSelectList, exp => exp.Ignore());

            Mapper.CreateMap<GameEditModel, Game>()
                  .MapEntityModifiable()
                  .ForMember(e => e.GameId, exp => exp.Ignore())
                  .ForMember(e => e.GameTypeId, exp => exp.MapFrom(m => m.GameType.GameTypeId))
                  .ForMember(e => e.GameType, exp => exp.Ignore())
                  .ForMember(e => e.GameStatusId, exp => exp.MapFrom(m => m.GameStatus.GameStatusId))
                  .ForMember(e => e.GameStatus, exp => exp.Ignore())
                  .ForMember(e => e.GameParticipants, exp => exp.Ignore());

            Mapper.CreateMap<GameParticipantEditModel, GameParticipant>()
                  .MapEntityModifiable()
                  .ForMember(e => e.GameParticipantId, exp => exp.Ignore())
                  .ForMember(e => e.GameId, exp => exp.Ignore())
                  .ForMember(e => e.Game, exp => exp.Ignore())
                  .ForMember(e => e.IsHost, exp => exp.Ignore())
                  .ForMember(e => e.TeamYearId, exp => exp.MapFrom(m => m.TeamYear.TeamYearId))
                  .ForMember(e => e.TeamYear, exp => exp.Ignore())
                  .ForMember(e => e.StatLines, exp => exp.Ignore());

            #endregion

            #region NewsItem

            Mapper.CreateMap<NewsItem, NewsEditTeamModel>()
                  .ForMember(m => m.IsReadonly, exp => exp.MapFrom(e => (DateTime.Now - e.Created) > TimeSpan.FromDays(30)))
                  .ForMember(m => m.TeamName, exp => exp.MapFrom(e => e.GetTeamName() ?? Consts.LeagueNameForList))
                  .ForMember(m => m.Teams, exp => exp.MapFrom(e => new List<NewsEditTeamListModel>()));

            Mapper.CreateMap<NewsItem, NewsEditModel>()
                  .MapEditModelBaseWithAudit()
                  .ForMember(m => m.TeamModel, exp => exp.MapFrom(e => Mapper.Map<NewsEditTeamModel>(e)));

            Mapper.CreateMap<NewsEditModel, NewsItem>()
                  .MapEntityModifiable()
                  .ForMember(e => e.TeamId, exp => exp.MapFrom(m => m.TeamModel.TeamId))
                  .ForMember(e => e.Team, exp => exp.Ignore())
                  .ForMember(e => e.Content, exp => exp.MapFrom(m => m.Content.Sanitize()));

            #endregion

            #region PageContent

            Mapper.CreateMap<PageContent, PageContentEditModel>()
                  .MapEditModelBaseWithAudit();

            Mapper.CreateMap<PageContentEditModel, PageContent>()
                  .MapEntityModifiable()
                  .ForMember(e => e.Content, exp => exp.MapFrom(m => m.Content.Sanitize()));

            #endregion

            #region Player

            Mapper.CreateMap<Player, PlayerEditModel>()
                  .MapEditModelBaseWithAudit()
                  .ForMember(m => m.Team, exp => exp.MapFrom(e => e.CurrentTeam))
                  .ForMember(m => m.HasStatsFor, exp => exp.MapFrom(e => e.StatLines
                                                                          .Select(sl => sl.GameParticipant.TeamYear)
                                                                          .Distinct()
                                                                          .GroupBy(ty => ty.Team)));

            Mapper.CreateMap<Team, PlayerEditTeamModel>()
                  .ForMember(m => m.TransferUrl, exp => exp.Ignore())
                  .ForMember(m => m.ItemSelectList, exp => exp.Ignore())
                  .ForMember(m => m.FullName, exp => exp.MapFrom(e => e.TeamYears.OrderByDescending(ty => ty.Year).First().FullName));

            Mapper.CreateMap<IGrouping<Team, TeamYear>, string>()
                  .ConvertUsing(g => $"{g.Key.TeamYears.OrderByDescending(ty => ty.Year).First().FullName} ({string.Join(", ", g.Select(ty => ty.Year).OrderByDescending(y => y))})");

            Mapper.CreateMap<PlayerEditModel, Player>()
                  .MapEntityModifiable()
                  .ForMember(e => e.PlayerId, exp => exp.Ignore())
                  .ForMember(e => e.CurrentTeamId, exp => exp.MapFrom(m => m.Team.TeamId))
                  .ForMember(e => e.CurrentTeam, exp => exp.Ignore())
                  .ForMember(e => e.StatLines, exp => exp.Ignore());

            #endregion

            #region Schedule

            Mapper.CreateMap<KeyValuePair<GameBucket, List<Game>>, ScheduleBucketEditModel>()
                  .ForMember(m => m.Bucket, exp => exp.MapFrom(kvp => kvp.Key))
                  .ForMember(m => m.Games, exp => exp.MapFrom(kvp => kvp.Value));

            Mapper.CreateMap<IGrouping<GameBucket, Game>, ScheduleBucketEditModel>()
                  .ForMember(m => m.Bucket, exp => exp.MapFrom(g => g.Key))
                  .ForMember(m => m.Games, exp => exp.MapFrom(g => g.ToList()));

            Mapper.CreateMap<Game, ScheduleGameEditModel>()
                  .ForMember(m => m.Entered, exp => exp.MapFrom(e => e.GameStatusId != GameStatus.Scheduled))
                  .ForMember(m => m.Outcome, exp => exp.MapFrom(e => e.GameStatus.Description))
                  .ForMember(m => m.RoadTeam, exp => exp.MapFrom(e => e.GameParticipants.SingleOrDefault(gp => !gp.IsHost)))
                  .ForMember(m => m.HomeTeam, exp => exp.MapFrom(e => e.GameParticipants.SingleOrDefault(gp => gp.IsHost)));

            Mapper.CreateMap<GameParticipant, ScheduleGameParticipantEditModel>()
                  .ForMember(m => m.TeamName, exp => exp.MapFrom(e => e.TeamYear.FullName));

            #endregion

            #region Team

            Mapper.CreateMap<TeamYear, TeamEditModel>()
                  .MapEditModelBaseWithAudit()
                  .ForMember(m => m.Conference, exp => exp.MapFrom(e => e.DivisionYear.ConferenceYear))
                  .ForMember(m => m.Division, exp => exp.MapFrom(e => e.DivisionYear))
                  .ForMember(m => m.Clinch, exp => exp.MapFrom(e => e == null ? new TeamEditClinchModel() : (object)e.Clinch))
                  .ForMember(m => m.FieldInformation, exp => exp.MapFrom(e => e.Team.FieldInformation))
                  .ForMember(m => m.Comments, exp => exp.MapFrom(e => e.Team.Comments));

            Mapper.CreateMap<TeamYear, TeamManageModel>()
                  .ForMember(m => m.Team, exp => exp.MapFrom(e => e));

            Mapper.CreateMap<ConferenceYear, TeamEditDivisionModel>()
                  .ForMember(m => m.DivisionYearId, exp => exp.Ignore())
                  .ForMember(m => m.ItemSelectList, exp => exp.Ignore());

            Mapper.CreateMap<DivisionYear, TeamEditDivisionModel>()
                  .ForMember(m => m.ItemSelectList, exp => exp.Ignore());

            Mapper.CreateMap<Church, TeamEditChurchModel>()
                  .ForMember(m => m.ItemSelectList, exp => exp.Ignore());

            Mapper.CreateMap<Coach, TeamEditCoachModel>()
                  .ForMember(m => m.ItemSelectList, exp => exp.Ignore())
                  .ForMember(m => m.SortableName, exp => exp.MapFrom(e => $"{e.LastName}, {e.FirstName}"));

            Mapper.CreateMap<string, TeamEditClinchModel>()
                  .ForMember(m => m.ClinchChar, exp => exp.MapFrom(s => s))
                  .ForMember(m => m.Description, exp => exp.MapFrom(s => s == null ? null : Consts.ClinchDescriptions[s]))
                  .ForMember(m => m.ItemSelectList, exp => exp.Ignore());

            Mapper.CreateMap<TeamEditModel, TeamYear>()
                  .MapEntityModifiable()
                  .ForMember(e => e.TeamId, exp => exp.Ignore())
                  .ForMember(e => e.Year, exp => exp.Ignore())
                  .ForMember(e => e.TeamYearId, exp => exp.Ignore())
                  .ForMember(e => e.Team, exp => exp.MapFrom(m => m))
                  .ForMember(e => e.FullName, exp => exp.Ignore())
                  .ForMember(e => e.ChurchId, exp => exp.MapFrom(m => m.Church.ChurchId))
                  .ForMember(e => e.Church, exp => exp.Ignore())
                  .ForMember(e => e.DivisionYearId, exp => exp.MapFrom(m => m.Division.DivisionYearId))
                  .ForMember(e => e.DivisionYear, exp => exp.Ignore())
                  .ForMember(e => e.HeadCoachId, exp => exp.MapFrom(m => m.HeadCoach.CoachId))
                  .ForMember(e => e.HeadCoach, exp => exp.Ignore())
                  .ForMember(e => e.Clinch, exp => exp.MapFrom(m => m.Clinch.ClinchChar))
                  .ForMember(e => e.GameParticipants, exp => exp.Ignore());

            Mapper.CreateMap<TeamEditModel, Team>()
                  .MapEntityModifiable()
                  .ForMember(e => e.TeamId, exp => exp.Ignore())
                  .ForMember(e => e.TeamYears, exp => exp.Ignore())
                  .ForMember(e => e.NewsItems, exp => exp.Ignore())
                  .ForMember(e => e.Players, exp => exp.Ignore())
                  .ForMember(e => e.ManagingUsers, exp => exp.Ignore());

            #endregion

            #region User Management

            Mapper.CreateMap<TcbcslUser, UserEditModel>()
                  .ForMember(m => m.Roles, exp => exp.MapFrom(e => new RolesEditModel {RoleIds = e.Roles.Select(r => r.RoleId).ToList()}))
                  .ForMember(m => m.AssignedTeams, exp => exp.MapFrom(e => new AssignedTeamsEditModel
                                                                           {
                                                                               TeamIds = e.AssignedTeams.Select(r => r.TeamId).ToList(),
                                                                               SelectedTeamNames = string.Join(", ", e.AssignedTeams.Select(t => Mapper.Map<string>(t.TeamYears.Last())))
                                                                           }))
                  .MapEditModelBase();

            Mapper.CreateMap<IEnumerable<IdentityRole>, string>()
                  .ConvertUsing(en => string.Join(", ", en.Select(r => r.Name)));

            Mapper.CreateMap<IdentityRole, SelectListItem>()
                  .ForMember(m => m.Value, exp => exp.MapFrom(e => e.Id))
                  .ForMember(m => m.Text, exp => exp.MapFrom(e => e.Name))
                  .IgnoreTheRest();

            Mapper.CreateMap<Team, SelectListItem>()
                  .ForMember(m => m.Value, exp => exp.MapFrom(e => e.TeamId))
                  .ForMember(m => m.Text, exp => exp.MapFrom(e => Mapper.Map<string>(e.TeamYears.OrderByDescending(ty => ty.Year).First())))
                  .IgnoreTheRest();

            Mapper.CreateMap<TeamYear, string>()
                  .ConvertUsing(ty => $"{ty.FullName} ({ty.Year})");

            Mapper.CreateMap<UserEditModel, TcbcslUser>()
                  .ForMember(e => e.Roles, exp => exp.MapFrom(m => m.Roles.RoleIds.Select(id => new IdentityUserRole {UserId = m.Id, RoleId = id})))
                  .ForMember(e => e.AssignedTeams, exp => exp.Ignore())
                  .ForMember(e => e.UserName, exp => exp.Ignore())
                  .IgnoreTheRest();

            #endregion

            Mapper.AssertConfigurationIsValid();
        }

        #region Mapping Extension Methods

        private static string GetTeamName(this NewsItem newsItem)
        {
            return newsItem.Team?
                           .TeamYears
                           .SingleOrDefault(ty => ty.Year == newsItem.StartDate.Year)?
                           .FullName;
        }

        private static IMappingExpression<TSource, TDestination> IgnoreTheRest<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var typeMap = Mapper.FindTypeMapFor<TSource, TDestination>();
            if (typeMap == null)
            {
                return expression;
            }

            foreach (var unmappedPropertyName in typeMap.GetUnmappedPropertyNames())
            {
                expression.ForMember(unmappedPropertyName, opt => opt.Ignore());
            }

            return expression;
        }

        private static IMappingExpression<TEntity, TModel> MapEditModelBase<TEntity, TModel>(this IMappingExpression<TEntity, TModel> mapping)
            where TModel : EditModelBase
        {
            return mapping.ForMember(m => m.EditUrl, exp => exp.Ignore());
        }

        private static IMappingExpression<TEntity, TModel> MapEditModelBaseWithAudit<TEntity, TModel>(this IMappingExpression<TEntity, TModel> mapping)
            where TEntity : EntityModifiable
            where TModel : EditModelBaseWithAudit
        {
            return mapping.ForMember(m => m.AuditDetails, exp => exp.MapFrom(e => Mapper.Map<AuditDetailsModel>(e)))
                          .MapEditModelBase();
        }

        private static IMappingExpression<TEntity, TModel> MapEditModelBaseWithContactInfo<TEntity, TModel>(this IMappingExpression<TEntity, TModel> mapping)
            where TEntity : EntityWithContactInfo
            where TModel : EditModelBaseWithContactInfo
        {
            return mapping.MapEditModelBaseWithAudit();
        }

        private static IMappingExpression<TModel, TEntity> MapEntityModifiable<TModel, TEntity>(this IMappingExpression<TModel, TEntity> mapping)
            where TModel : EditModelBaseWithAudit
            where TEntity : EntityModifiable
        {
            return mapping.ForMember(e => e.CreatedBy, exp => exp.Ignore())
                          .ForMember(e => e.Created, exp => exp.Ignore())
                          .ForMember(e => e.Modified, exp => exp.Ignore())
                          .ForMember(e => e.ModifiedBy, exp => exp.Ignore());
        }

        private static IMappingExpression<TModel, TEntity> MapEntityWithContactInfo<TModel, TEntity>(this IMappingExpression<TModel, TEntity> mapping)
            where TModel : EditModelBaseWithContactInfo
            where TEntity : EntityWithContactInfo
        {
            return mapping.MapEntityModifiable()
                          .ForMember(e => e.AddressId, exp => exp.Ignore())
                          .ForMember(e => e.PhoneNumbers, exp => exp.Ignore());
        }

        #endregion
    }
}