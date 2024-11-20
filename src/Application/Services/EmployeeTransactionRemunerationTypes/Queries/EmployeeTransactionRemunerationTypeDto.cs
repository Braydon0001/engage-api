namespace Engage.Application.Services.EmployeeTransactionRemunerationTypes.Queries;

public class EmployeeTransactionRemunerationTypeDto : IMapFrom<EmployeeTransactionRemunerationType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeTransactionRemunerationType, EmployeeTransactionRemunerationTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeTransactionRemunerationTypeId));
    }
}
