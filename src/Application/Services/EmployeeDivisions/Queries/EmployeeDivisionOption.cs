namespace Engage.Application.Services.EmployeeDivisions.Queries;

public class EmployeeDivisionOption : IMapFrom<EmployeeDivision>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeDivision, EmployeeDivisionOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeDivisionId));
    }
}