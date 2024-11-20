// auto-generated
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.WebPages.Queries;

namespace Engage.Application.Services.WebPageEmployees.Queries;

public class WebPageEmployeeVm : IMapFrom<WebPageEmployee>
{
    public int Id { get; set; }
    public EmployeeOption EmployeeId { get; set; }
    public WebPageOption WebPageId { get; set; }
    public string ViewDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebPageEmployee, WebPageEmployeeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebPageEmployeeId))
               .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Employee))
               .ForMember(d => d.WebPageId, opt => opt.MapFrom(s => s.WebPage));
    }
}
