
using Engage.Application.Services.CostCenters.Queries;
using Engage.Application.Services.CostDepartments.Queries;

namespace Engage.Application.Services.CostCenterDepartments.Queries;

public class CostCenterDepartmentVm : IMapFrom<CostCenterDepartment>
{
    public int Id { get; init; }
    public CostCenterOption CostCenterId { get; init; }
    public CostDepartmentOption CostDepartmentId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenterDepartment, CostCenterDepartmentVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostCenterDepartmentId))
               .ForMember(d => d.CostCenterId, opt => opt.MapFrom(s => s.CostCenter))
               .ForMember(d => d.CostDepartmentId, opt => opt.MapFrom(s => s.CostDepartment));
    }
}
