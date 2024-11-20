namespace Engage.Application.Services.Budgets.Commands;

public class UpdateBudgetCommand : BudgetCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateBudgetCommandHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UpdateBudgetCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(UpdateBudgetCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Budgets.SingleAsync(x => x.BudgetId == command.Id, cancellationToken);
        Mapper.Map(command, entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.BudgetId;
        return opStatus;
    }
}
