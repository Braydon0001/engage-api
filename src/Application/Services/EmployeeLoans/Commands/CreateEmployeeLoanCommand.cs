namespace Engage.Application.Services.EmployeeLoans.Commands;

public class CreateEmployeeLoanCommand : EmployeeLoanCommand, IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
}

public class CreateEmployeeLoanCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeLoanCommand, OperationStatus>
{
    public CreateEmployeeLoanCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateEmployeeLoanCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEmployeeLoanCommand, EmployeeLoan>(command);
        entity.EmployeeId = command.EmployeeId;
        _context.EmployeeLoans.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeLoanId;
        return opStatus;
    }
}
