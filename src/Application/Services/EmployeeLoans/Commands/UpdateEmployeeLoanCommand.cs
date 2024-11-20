namespace Engage.Application.Services.EmployeeLoans.Commands;

public class UpdateEmployeeLoanCommand : EmployeeLoanCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeLoanCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeLoanCommand, OperationStatus>
{
    public UpdateEmployeeLoanCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateEmployeeLoanCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeLoans.SingleAsync(x => x.EmployeeLoanId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeLoanId;
        return opStatus;
    }
}
