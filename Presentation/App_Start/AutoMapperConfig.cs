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
            Mapper.CreateMap<EntityModifiable, AuditDetailsModel>()
                  .ForMember(m => m.Created, exp => exp.MapFrom(e => $"{e.Created:u}"))
                  .ForMember(m => m.Modified, exp => exp.MapFrom(e => $"{e.Modified:u}"));

            Mapper.CreateMap<PageContent, PageContentEditModel>()
                  .ForMember(m => m.EditUrl, exp => exp.Ignore())
                  .ForMember(m => m.AuditDetails, exp => exp.MapFrom(pc => Mapper.Map<AuditDetailsModel>(pc)));

            Mapper.CreateMap<PageContentEditModel, PageContent>()
                  .ForMember(pc => pc.CreatedBy, exp => exp.Ignore())
                  .ForMember(pc => pc.Created, exp => exp.Ignore())
                  .ForMember(pc => pc.Modified, exp => exp.Ignore())
                  .ForMember(pc => pc.ModifiedBy, exp => exp.Ignore());

            Mapper.CreateMap<NewsItem, NewsEditModel>()
                  .ForMember(m => m.TeamName, exp => exp.MapFrom(n => GetTeamNameFromNewsItem(n)))
                  .ForMember(m => m.StartDate, exp => exp.MapFrom(n => $"{n.StartDate:u}"))
                  .ForMember(m => m.EndDate, exp => exp.MapFrom(n => $"{n.EndDate:u}"))
                  .ForMember(m => m.EditUrl, exp => exp.Ignore())
                  .ForMember(m => m.AuditDetails, exp => exp.MapFrom(n => Mapper.Map<AuditDetailsModel>(n)));

            Mapper.CreateMap<NewsEditModel, NewsItem>()
                  .ForMember(pc => pc.Team, exp => exp.Ignore())
                  .ForMember(pc => pc.CreatedBy, exp => exp.Ignore())
                  .ForMember(pc => pc.Created, exp => exp.Ignore())
                  .ForMember(pc => pc.Modified, exp => exp.Ignore())
                  .ForMember(pc => pc.ModifiedBy, exp => exp.Ignore());

            Mapper.AssertConfigurationIsValid();
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