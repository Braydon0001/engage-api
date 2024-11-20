namespace Engage.Application.Services.BudgetYears.Commands;

public class CreateBudgetYearCommand : BudgetYearCommand, IRequest<OperationStatus>
{

}

public class CreateBudgetYearCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateBudgetYearCommand, OperationStatus>
{
    public CreateBudgetYearCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateBudgetYearCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateBudgetYearCommand, BudgetYear>(command);
        _context.BudgetYears.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.BudgetYearId;
        return opStatus;
    }
}
