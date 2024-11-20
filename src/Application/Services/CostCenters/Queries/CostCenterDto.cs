namespace Engage.Application.Services.CostCenters.Queries;

public class CostCenterDto : IMapFrom<CostCenter>
{
    public int Id { get; init; }
    public int CostTypeId { get; init; }
    public string CostTypeName { get; init; }
    public string Name { get; init; }
    public string CostCenterDepartmentNames { get; init; }
    public string CostCenterEmployeeNames { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenter, CostCenterDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostCenterId))
               .ForMember(d => d.CostCenterDepartmentNames, opt => opt.MapFrom(s => string.Join(", ", s.CostCenterDepartments.Select(d => d.CostDepartment.Name))))
               .ForMember(d => d.CostCenterEmployeeNames, opt => opt.MapFrom(s => string.Join(", ", s.CostCenterEmployees.Select(e => $"{e.Employee.FirstName} {e.Employee.LastName} - {e.Employee.Code}"))));
    }
}
