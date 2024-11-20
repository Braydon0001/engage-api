namespace Engage.Application.Services.BudgetPeriods.Commands;

public class BudgetPeriodCommand : IMapTo<BudgetPeriod>
{
    public int BudgetYearId { get; set; }
    public int BudgetId { get; set; }
    public int No { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BudgetPeriodCommand, BudgetPeriod>();
    }
}
