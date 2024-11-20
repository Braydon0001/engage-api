namespace Engage.Application.Services.BudgetYearVersions.Commands;

public class UpdateBudgetYearVersionCommand : BudgetYearVersionCommand, IRequest<OperationStatus>
{
}

public class UpdateBudgetYearVersionCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateBudgetYearVersionCommand, OperationStatus>
{
    public UpdateBudgetYearVersionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateBudgetYearVersionCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.BudgetYearVersions.SingleAsync(x => x.BudgetYearId == command.BudgetYearId &&
                                                                        x.BudgetVersionId == command.BudgetVersionId, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = new
        {
            entity.BudgetYearId,
            entity.BudgetVersionId
        };
        return opStatus;
    }
}
