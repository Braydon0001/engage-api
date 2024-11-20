namespace Engage.Application.Services.EmployeeExpenseClaims.Commands;

public class CreateEmployeeExpenseClaimCommand : EmployeeExpenseClaimCommand, IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
}

public class CreateEmployeeExpenseClaimCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeExpenseClaimCommand, OperationStatus>
{
    public CreateEmployeeExpenseClaimCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateEmployeeExpenseClaimCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEmployeeExpenseClaimCommand, EmployeeExpenseClaim>(command);
        entity.EmployeeId = command.EmployeeId;
        _context.EmployeeExpenseClaims.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeExpenseClaimId;
        return opStatus;
    }
}
