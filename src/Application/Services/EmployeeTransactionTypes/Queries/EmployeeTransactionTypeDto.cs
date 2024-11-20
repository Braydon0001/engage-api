// auto-generated
namespace Engage.Application.Services.EmployeeTransactionTypes.Queries;

public class EmployeeTransactionTypeDto : IMapFrom<EmployeeTransactionType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsPositive { get; set; }
    public bool IsRecurring { get; set; }
    public List<JsonEmployeeField> Fields { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransactionType, EmployeeTransactionTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeTransactionTypeId));
    }
}
