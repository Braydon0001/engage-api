namespace Engage.Application.Services.BudgetPeriods.Commands;

public class UpdateBudgetPeriodCommand : BudgetPeriodCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateBudgetPeriodCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateBudgetPeriodCommand, OperationStatus>
{
    public UpdateBudgetPeriodCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateBudgetPeriodCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.BudgetPeriods.SingleAsync(x => x.BudgetPeriodId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.BudgetPeriodId;
        return opStatus;
    }
}
