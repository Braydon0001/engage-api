
namespace Engage.Application.Services.EmployeeTransactionRemunerationTypes.Queries;

public class EmployeeTransactionRemunerationTypeVm : IMapFrom<EmployeeTransactionRemunerationType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransactionRemunerationType, EmployeeTransactionRemunerationTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeTransactionRemunerationTypeId));
    }
}
