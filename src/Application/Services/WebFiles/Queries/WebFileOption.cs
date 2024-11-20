namespace Engage.Application.Services.WebFiles.Queries;

public class WebFileOption : BaseOption, IMapFrom<WebFile>
{

    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebFile, WebFileOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebFileId));
    }
}