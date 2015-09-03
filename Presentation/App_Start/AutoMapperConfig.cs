using AutoMapper;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;

namespace Tcbcsl.Presentation
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<PageContent, PageContentEditModel>()
                  .ForMember(m => m.EditUrl, exp => exp.MapFrom(pc => $"/Admin/Conent/Edit/{pc.PageContentId}"));

            Mapper.CreateMap<PageContentEditModel, PageContent>()
                  .ForMember(pc => pc.PageContentId, exp => exp.Ignore());
        }
    }
}