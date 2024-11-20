// auto-generated
namespace Engage.Application.Services.WebFileCategories.Queries;

public class WebFileCategoryOption : BaseOption, IMapFrom<WebFileCategory>
{
    public int WebFileGroupId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebFileCategory, WebFileCategoryOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebFileCategoryId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => $"{s.WebFileGroup.Name} Group - {s.Name}"));
    }
}