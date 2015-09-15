using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using Tcbcsl.Data.Entities;
using Tcbcsl.Data.Identity;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;

namespace Tcbcsl.Presentation
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            #region Base mappings

            Mapper.CreateMap<EntityModifiable, AuditDetailsModel>();

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

            #region Users

            Mapper.CreateMap<TcbcslUser, UserEditModel>()
                  .MapEditModelBase();

            Mapper.CreateMap<UserEditModel, TcbcslUser>()
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

        private static IMappingExpression<TModel, TEntity> MapEntityModifiable<TModel, TEntity>(this IMappingExpression<TModel, TEntity> mapping)
            where TEntity : EntityModifiable
            where TModel : EditModelBaseWithAudit
        {
            return mapping.ForMember(e => e.CreatedBy, exp => exp.Ignore())
                          .ForMember(e => e.Created, exp => exp.Ignore())
                          .ForMember(e => e.Modified, exp => exp.Ignore())
                          .ForMember(e => e.ModifiedBy, exp => exp.Ignore());
        }

        #endregion
    }
}