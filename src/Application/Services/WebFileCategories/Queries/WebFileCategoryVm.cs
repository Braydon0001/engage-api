// auto-generated
using Engage.Application.Services.WebFileGroups.Queries;

namespace Engage.Application.Services.WebFileCategories.Queries;

public class WebFileCategoryVm : IMapFrom<WebFileCategory>
{
    public int Id { get; set; }
    public WebFileGroupOption WebFileGroupId { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public int Order { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebFileCategory, WebFileCategoryVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebFileCategoryId))
               .ForMember(d => d.WebFileGroupId, opt => opt.MapFrom(s => s.WebFileGroup));
    }
}
