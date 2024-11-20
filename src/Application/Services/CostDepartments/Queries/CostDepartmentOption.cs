namespace Engage.Application.Services.CostDepartments.Queries;

public class CostDepartmentOption : IMapFrom<CostDepartment>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostDepartment, CostDepartmentOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostDepartmentId));
    }
}