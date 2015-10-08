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

            Mapper.CreateMap<State, StateModel>();

            Mapper.CreateMap<State, StateEditModel>()
                  .ForMember(m => m.StateName, exp => exp.MapFrom(e => e.Name))
                  .ForMember(m => m.States, exp => exp.Ignore());

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