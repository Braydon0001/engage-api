namespace Engage.Application.Services.CostSubDepartments.Queries;

public class CostSubDepartmentDto : IMapFrom<CostSubDepartment>
{
    public int Id { get; init; }
    public int CostDepartmentId { get; init; }
    public string CostDepartmentName { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostSubDepartment, CostSubDepartmentDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostSubDepartmentId));
    }
}
