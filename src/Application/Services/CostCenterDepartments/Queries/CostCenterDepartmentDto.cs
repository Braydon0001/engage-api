namespace Engage.Application.Services.CostCenterDepartments.Queries;

public class CostCenterDepartmentDto : IMapFrom<CostCenterDepartment>
{
    public int Id { get; init; }
    public int CostCenterId { get; init; }
    public int CostDepartmentId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenterDepartment, CostCenterDepartmentDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostCenterDepartmentId));
    }
}
