using AutoMapper;
using System.Linq;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;

namespace Tcbcsl.Presentation
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<EntityModifiable, AuditDetailsModel>();

            Mapper.CreateMap<PageContent, PageContentEditModel>()
                  .MapEditModelBase();

            Mapper.CreateMap<PageContentEditModel, PageContent>()
                  .MapEntityModifiable();

            Mapper.CreateMap<NewsItem, NewsEditModel>()
                  .MapEditModelBase()
                  .ForMember(m => m.TeamName, exp => exp.MapFrom(n => GetTeamNameFromNewsItem(n)));

            Mapper.CreateMap<NewsEditModel, NewsItem>()
                  .MapEntityModifiable()
                  .ForMember(pc => pc.Team, exp => exp.Ignore());

            Mapper.AssertConfigurationIsValid();
        }

        private static IMappingExpression<TEntity, TModel> MapEditModelBase<TEntity, TModel>(this IMappingExpression<TEntity, TModel> mapping)
            where TEntity : EntityModifiable
            where TModel : EditModelBase
        {
            return mapping.ForMember(m => m.EditUrl, exp => exp.Ignore())
                          .ForMember(m => m.AuditDetails, exp => exp.MapFrom(e => Mapper.Map<AuditDetailsModel>(e)));
        }

        private static IMappingExpression<TModel, TEntity> MapEntityModifiable<TModel, TEntity>(this IMappingExpression<TModel, TEntity> mapping)
            where TEntity : EntityModifiable
            where TModel : EditModelBase
        {
            return mapping.ForMember(e => e.CreatedBy, exp => exp.Ignore())
                          .ForMember(e => e.Created, exp => exp.Ignore())
                          .ForMember(e => e.Modified, exp => exp.Ignore())
                          .ForMember(e => e.ModifiedBy, exp => exp.Ignore());
        }

        private static string GetTeamNameFromNewsItem(NewsItem n)
        {
            return n.Team?
                    .TeamYears
                    .SingleOrDefault(ty => ty.Year == n.StartDate.Year)?
                    .FullName
                ?? "League";
        }
    }
}