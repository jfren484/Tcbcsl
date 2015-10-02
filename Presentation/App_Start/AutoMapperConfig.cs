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

// ReSharper disable UnusedMethodReturnValue.Local

namespace Tcbcsl.Presentation
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            #region Base mappings

            Mapper.CreateMap<EntityModifiable, AuditDetailsModel>();

            Mapper.CreateMap<EntityWithContactInfo, ContactInfoEditModel>();

            #endregion

            #region Church

            Mapper.CreateMap<Church, ChurchEditModel>()
                  .MapEditModelBaseWithContactInfo();

            //Mapper.CreateMap<ChurchEditModel, Church>()
            //      .MapEntityWithContactInfo()
            //      .ForMember(m => m.TeamYears, exp => exp.Ignore());

            #endregion

            #region Contact Info mappings

            Mapper.CreateMap<Address, AddressEditModel>();

            Mapper.CreateMap<ContactPhoneNumber, PhoneEditModel>()
                  .ForMember(m => m.PhoneTypeName, exp => exp.MapFrom(e => e.PhoneNumberType.Description))
                  .ForMember(m => m.PhoneTypes, exp => exp.Ignore());

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
            return mapping.ForMember(m => m.ContactInfo, exp => exp.MapFrom(e => Mapper.Map<ContactInfoEditModel>(e)))
                          .MapEditModelBaseWithAudit();
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
                          .ForMember(e => e.AddressId, exp => exp.MapFrom(m => m.ContactInfo.Address.AddressId))
                          .ForMember(e => e.Address, exp => exp.MapFrom(m => m.ContactInfo.Address))
                          .ForMember(e => e.EmailAddress, exp => exp.MapFrom(m => m.ContactInfo.EmailAddress))
                          .ForMember(e => e.PhoneNumbers, exp => exp.Ignore())
                //.ForMember(e => e.Street1, exp => exp.MapFrom(m => m.ContactInfo.Street1))
                //.ForMember(e => e.Street2, exp => exp.MapFrom(m => m.ContactInfo.Street2))
                //.ForMember(e => e.City, exp => exp.MapFrom(m => m.ContactInfo.City))
                //.ForMember(e => e.Zip, exp => exp.MapFrom(m => m.ContactInfo.Zip))
                //.ForMember(e => e.Email, exp => exp.MapFrom(m => m.ContactInfo.Email))
                //.ForMember(e => e.State, exp => exp.Ignore())
                //.ForMember(e => e.StateId, exp => exp.MapFrom(m => m.ContactInfo.State.StateId))
                //.ForMember(e => e.Phone1Type, exp => exp.Ignore())
                //.ForMember(e => e.Phone1TypeId, exp => exp.MapFrom(m => m.ContactInfo.PrimaryPhone.PhoneTypeId))
                //.ForMember(e => e.Phone1, exp => exp.MapFrom(m => m.ContactInfo.PrimaryPhone.PhoneNumber))
                //.ForMember(e => e.Phone2Type, exp => exp.Ignore())
                //.ForMember(e => e.Phone2TypeId, exp => exp.MapFrom(m => m.ContactInfo.SecondaryPhone.PhoneTypeId))
                //.ForMember(e => e.Phone2, exp => exp.MapFrom(m => m.ContactInfo.SecondaryPhone.PhoneNumber))
                ;
        }

        #endregion
    }
}