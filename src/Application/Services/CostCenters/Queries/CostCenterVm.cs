using Engage.Application.Services.CostTypes.Queries;

namespace Engage.Application.Services.CostCenters.Queries;

public class CostCenterVm : IMapFrom<CostCenter>
{
    public int Id { get; init; }
    public CostTypeOption CostTypeId { get; init; }
    public string Name { get; init; }
    public List<OptionDto> CostDepartmentIds { get; init; }
    public List<OptionDto> CostEmployeeIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenter, CostCenterVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostCenterId))
               .ForMember(d => d.CostTypeId, opt => opt.MapFrom(s => s.CostType))
               .ForMember(d => d.CostDepartmentIds, opt => opt.MapFrom(s => s.CostCenterDepartments.Select(d => new OptionDto(d.CostDepartmentId, d.CostDepartment.Name)).ToList()))
               .ForMember(d => d.CostEmployeeIds, opt => opt.MapFrom(s => s.CostCenterEmployees.Select(e => new OptionDto(e.EmployeeId, $"{e.Employee.FirstName} {e.Employee.LastName} - {e.Employee.Code}")).ToList()));
    }
}
