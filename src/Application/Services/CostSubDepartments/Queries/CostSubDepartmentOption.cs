namespace Engage.Application.Services.CostSubDepartments.Queries;

public class CostSubDepartmentOption : IMapFrom<CostSubDepartment>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostSubDepartment, CostSubDepartmentOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CostSubDepartmentId));
    }
}