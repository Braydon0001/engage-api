namespace Engage.Application.Services.EmployeeTransactionRemunerationTypes.Queries;

public class EmployeeTransactionRemunerationTypeOption : IMapFrom<EmployeeTransactionRemunerationType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransactionRemunerationType, EmployeeTransactionRemunerationTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeTransactionRemunerationTypeId));
    }
}