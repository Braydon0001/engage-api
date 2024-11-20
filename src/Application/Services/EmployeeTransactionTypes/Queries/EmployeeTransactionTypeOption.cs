// auto-generated
namespace Engage.Application.Services.EmployeeTransactionTypes.Queries;

public class EmployeeTransactionTypeOption : IMapFrom<EmployeeTransactionType>
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; }
    public List<JsonEmployeeField> Fields { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransactionType, EmployeeTransactionTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeTransactionTypeId))
               .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.EmployeeTransactionTypeGroupId));
    }
}