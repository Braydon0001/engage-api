namespace Engage.Application.Services.Budgets.Commands;

public record EngageRegionBudgetBulkUpdateCommand(Dictionary<int, float> Updates) : IRequest<OperationStatus>;

public class EngageRegionBudgetBulkUpdateHandler(IAppDbContext Context) : IRequestHandler<EngageRegionBudgetBulkUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(EngageRegionBudgetBulkUpdateCommand command, CancellationToken cancellationToken)
    {
        foreach (var update in command.Updates)
        {
            var budget = await Context.Budgets.FirstOrDefaultAsync(e => e.BudgetId == update.Key, cancellationToken);
            if (budget != null)
            {
                budget.Value = update.Value;
            }
        };

        return await Context.SaveChangesAsync(cancellationToken);
    }
}
