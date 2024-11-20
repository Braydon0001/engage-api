namespace Engage.Application.Services.EmployeeHealthConditions.Queries;

public class EmployeeHealthConditionDto : IMapFrom<EmployeeHealthCondition>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeHealthCondition, EmployeeHealthConditionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeHealthConditionId));
    }
}
