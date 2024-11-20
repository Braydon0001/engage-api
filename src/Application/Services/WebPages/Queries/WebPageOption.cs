// auto-generated
namespace Engage.Application.Services.WebPages.Queries;

public class WebPageOption : IMapFrom<WebPage>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebPage, WebPageOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebPageId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
    }
}