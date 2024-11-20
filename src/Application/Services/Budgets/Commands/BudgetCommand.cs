namespace Engage.Application.Services.Budgets.Commands;

public class BudgetCommand : IMapTo<Budget>
{
    public int GLAccountId { get; set; }
    public int BudgetTypeId { get; set; }
    public int BudgetYearId { get; set; }
    public int BudgetVersionId { get; set; }
    public int BudgetPeriodId { get; set; }
    public double Value { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BudgetCommand, Budget>();
    }
}
