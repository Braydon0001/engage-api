// auto-generated
namespace Engage.Application.Services.WebFileCategories.Queries;

public class WebFileCategoryDto : IMapFrom<WebFileCategory>
{
    public int Id { get; set; }
    public int WebFileGroupId { get; set; }
    public string WebFileGroupName { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public int Order { get; set; }
    public int WebFileCount { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebFileCategory, WebFileCategoryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebFileCategoryId))
               .ForMember(d => d.WebFileCount, opt => opt.MapFrom(s => s.WebFiles.Count));

    }
}
