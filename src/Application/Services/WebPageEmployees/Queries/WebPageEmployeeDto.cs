// auto-generated
namespace Engage.Application.Services.WebPageEmployees.Queries;

public class WebPageEmployeeDto : IMapFrom<WebPageEmployee>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int WebPageId { get; set; }
    public string WebPageName { get; set; }
    public string ViewDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebPageEmployee, WebPageEmployeeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebPageEmployeeId))
               .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName));
    }
}
