namespace Engage.Application.Services.CostCenterDepartments.Queries;

public class CostCenterDepartmentOption : IMapFrom<CostCenterDepartment>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenterDepartment, CostCenterDepartmentOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostCenterDepartmentId));
    }
}