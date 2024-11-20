namespace Engage.Application.Services.BudgetYears.Commands;

public class UpdateBudgetYearCommand : BudgetYearCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateBudgetYearCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateBudgetYearCommand, OperationStatus>
{
    public UpdateBudgetYearCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateBudgetYearCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.BudgetYears.SingleAsync(x => x.BudgetYearId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.BudgetYearId;
        return opStatus;
    }
}
