
using Engage.Application.Services.CostDepartments.Queries;

namespace Engage.Application.Services.CostSubDepartments.Queries;

public class CostSubDepartmentVm : IMapFrom<CostSubDepartment>
{
    public int Id { get; init; }
    public CostDepartmentOption CostDepartmentId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostSubDepartment, CostSubDepartmentVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostSubDepartmentId))
               .ForMember(d => d.CostDepartmentId, opt => opt.MapFrom(s => s.CostDepartment));
    }
}
