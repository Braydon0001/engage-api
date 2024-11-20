namespace Engage.Application.Services.ExpenseTypes.Queries;

public class ExpenseTypeOption : IMapFrom<ExpenseType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ExpenseType, ExpenseTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ExpenseTypeId));
    }
}