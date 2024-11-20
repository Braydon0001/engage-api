namespace Engage.Application.Services.Budgets.Models;

public class BudgetDto : IMapFrom<Budget>
{
    public int Id { get; set; }
    public int GLAccountId { get; set; }
    public int BudgetTypeId { get; set; }
    public int BudgetYearId { get; set; }
    public int BudgetVersionId { get; set; }
    public int BudgetPeriodId { get; set; }
    public double Value { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Budget, BudgetDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.BudgetId));
    }
}
