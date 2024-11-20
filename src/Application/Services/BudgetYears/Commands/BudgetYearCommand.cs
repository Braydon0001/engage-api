namespace Engage.Application.Services.BudgetYears.Commands;

public class BudgetYearCommand : IMapTo<BudgetYear>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BudgetYearCommand, BudgetYear>();
    }
}
