namespace Engage.Application.Services.EmployeeExpenseClaims.Commands;

public class UpdateEmployeeExpenseClaimCommand : EmployeeExpenseClaimCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeExpenseClaimCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeExpenseClaimCommand, OperationStatus>
{
    public UpdateEmployeeExpenseClaimCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateEmployeeExpenseClaimCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeExpenseClaims.SingleAsync(x => x.EmployeeExpenseClaimId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeExpenseClaimId;
        return opStatus;
    }
}
