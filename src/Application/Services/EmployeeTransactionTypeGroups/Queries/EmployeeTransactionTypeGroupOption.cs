namespace Engage.Application.Services.EmployeeTransactionTypeGroups.Queries;

public class EmployeeTransactionTypeGroupOption : IMapFrom<EmployeeTransactionTypeGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransactionTypeGroup, EmployeeTransactionTypeGroupOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeTransactionTypeGroupId));
    }
}