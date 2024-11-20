// auto-generated
namespace Engage.Application.Services.WebPageEmployees.Queries;

public class WebPageEmployeeOption : IMapFrom<WebPageEmployee>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebPageEmployee, WebPageEmployeeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebPageEmployeeId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ViewDate + " - " + s.WebPage.Name));
    }
}