namespace Engage.Application.Services.CostCenterEmployees.Queries;

public class CostCenterEmployeeOption : IMapFrom<CostCenterEmployee>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenterEmployee, CostCenterEmployeeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostCenterEmployeeId));
    }
}