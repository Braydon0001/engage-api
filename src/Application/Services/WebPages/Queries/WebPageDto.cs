// auto-generated
namespace Engage.Application.Services.WebPages.Queries;

public class WebPageDto : IMapFrom<WebPage>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebPage, WebPageDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebPageId));
    }
}
