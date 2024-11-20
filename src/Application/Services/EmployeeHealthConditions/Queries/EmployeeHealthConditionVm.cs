namespace Engage.Application.Services.EmployeeHealthConditions.Queries;

public class EmployeeHealthConditionVm : IMapFrom<EmployeeHealthCondition>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeHealthCondition, EmployeeHealthConditionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeHealthConditionId));
    }
}
