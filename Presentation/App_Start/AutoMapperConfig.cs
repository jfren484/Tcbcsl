using AutoMapper;
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
                  .ForMember(m => m.EditUrl, exp => exp.Ignore())
                  .ForMember(m => m.AuditDetails, exp => exp.MapFrom(pc => Mapper.Map<AuditDetailsModel>(pc)));

            Mapper.CreateMap<PageContentEditModel, PageContent>()
                  .ForMember(pc => pc.CreatedBy, exp => exp.Ignore())
                  .ForMember(pc => pc.Created, exp => exp.Ignore())
                  .ForMember(pc => pc.Modified, exp => exp.Ignore())
                  .ForMember(pc => pc.ModifiedBy, exp => exp.Ignore());

            Mapper.AssertConfigurationIsValid();
        }
    }
}