// auto-generated
namespace Engage.Application.Services.WebFileGroups.Queries;

public class WebFileGroupOption : BaseOption, IMapFrom<WebFileGroup>
{

    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebFileGroup, WebFileGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebFileGroupId));
    }
}