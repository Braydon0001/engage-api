namespace Engage.Application.Services.CostDepartments.Queries;

public class CostDepartmentDto : IMapFrom<CostDepartment>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string CostSubDepartmentNames { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostDepartment, CostDepartmentDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostDepartmentId))
               .ForMember(d => d.CostSubDepartmentNames, opt => opt.MapFrom(s => string.Join(", ", s.CostSubDepartments.Select(d => d.Name))));
    }
}
