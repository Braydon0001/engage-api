namespace Engage.Application.Services.CostCenterEmployees.Queries;

public class CostCenterEmployeeDto : IMapFrom<CostCenterEmployee>
{
    public int Id { get; init; }
    public int CostCenterId { get; init; }
    public int EmployeeId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenterEmployee, CostCenterEmployeeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostCenterEmployeeId));
    }
}
