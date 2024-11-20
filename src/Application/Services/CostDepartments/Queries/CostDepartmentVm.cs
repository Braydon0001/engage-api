
namespace Engage.Application.Services.CostDepartments.Queries;

public class CostDepartmentVm : IMapFrom<CostDepartment>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostDepartment, CostDepartmentVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostDepartmentId));
    }
}
