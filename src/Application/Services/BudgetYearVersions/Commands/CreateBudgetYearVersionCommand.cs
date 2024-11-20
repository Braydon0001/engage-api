namespace Engage.Application.Services.BudgetYearVersions.Commands;

public class CreateBudgetYearVersionCommand : BudgetYearVersionCommand, IRequest<OperationStatus>
{

}

public class CreateBudgetYearVersionCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateBudgetYearVersionCommand, OperationStatus>
{
    public CreateBudgetYearVersionCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateBudgetYearVersionCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateBudgetYearVersionCommand, BudgetYearVersion>(command);
        _context.BudgetYearVersions.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = new { entity.BudgetYearId, entity.BudgetVersionId };
        return opStatus;
    }
}
