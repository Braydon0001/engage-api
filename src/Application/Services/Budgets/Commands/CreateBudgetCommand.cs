namespace Engage.Application.Services.Budgets.Commands;

public class CreateBudgetCommand : BudgetCommand, IRequest<OperationStatus>
{

}

public class CreateBudgetCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateBudgetCommand, OperationStatus>
{
    public CreateBudgetCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateBudgetCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateBudgetCommand, Budget>(command);
        _context.Budgets.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.BudgetId;
        return opStatus;
    }
}
