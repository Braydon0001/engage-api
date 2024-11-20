namespace Engage.Application.Services.Employees.Queries;

public class EmployeeOption : BaseOption, IMapFrom<Employee>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName} - {s.Code}"));
    }
}
