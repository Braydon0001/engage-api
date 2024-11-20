namespace Engage.Application.Services.BudgetPeriods.Commands;

public class CreateBudgetPeriodCommand : BudgetPeriodCommand, IRequest<OperationStatus>
{
}

public class CreateBudgetPeriodCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateBudgetPeriodCommand, OperationStatus>
{
    public CreateBudgetPeriodCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateBudgetPeriodCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateBudgetPeriodCommand, BudgetPeriod>(command);
        _context.BudgetPeriods.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.BudgetPeriodId;
        return opStatus;
    }
}
