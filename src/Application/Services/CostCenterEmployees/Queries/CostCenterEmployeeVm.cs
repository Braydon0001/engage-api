
using Engage.Application.Services.CostCenters.Queries;
using Engage.Application.Services.Employees.Queries;

namespace Engage.Application.Services.CostCenterEmployees.Queries;

public class CostCenterEmployeeVm : IMapFrom<CostCenterEmployee>
{
    public int Id { get; init; }
    public CostCenterOption CostCenterId { get; init; }
    public EmployeeOption EmployeeId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenterEmployee, CostCenterEmployeeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostCenterEmployeeId))
               .ForMember(d => d.CostCenterId, opt => opt.MapFrom(s => s.CostCenter))
               .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Employee));
    }
}
