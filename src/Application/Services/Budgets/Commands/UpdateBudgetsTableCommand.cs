using Engage.Application.Services.Budgets.Models;

namespace Engage.Application.Services.Budgets.Commands;

public class UpdateBudgetsTableCommand : IRequest<OperationStatus>
{
    public ICollection<BudgetDto> Data { get; set; }

    public UpdateBudgetsTableCommand()
    {
        Data = new HashSet<BudgetDto>();
    }
}

public class UpdateBudgetsTableCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateBudgetsTableCommand, OperationStatus>
{
    public UpdateBudgetsTableCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateBudgetsTableCommand command, CancellationToken cancellationToken)
    {
        var budgets = new List<Budget>();
        foreach (var item in command.Data)
        {
            var budget = new Budget
            {
                BudgetId = item.Id,
                GLAccountId = item.GLAccountId,
                BudgetTypeId = item.BudgetTypeId,
                BudgetYearId = item.BudgetYearId,
                BudgetVersionId = item.BudgetVersionId,
                BudgetPeriodId = item.BudgetPeriodId,
                Value = item.Value
            };

            budgets.Add(budget);
        }

        _context.Budgets.UpdateRange(budgets);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}
