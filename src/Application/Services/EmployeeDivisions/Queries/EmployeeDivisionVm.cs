namespace Engage.Application.Services.EmployeeDivisions.Queries;

public class EmployeeDivisionVm : IMapFrom<EmployeeDivision>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsRihCallCycles { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeDivision, EmployeeDivisionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeDivisionId));
    }
}
