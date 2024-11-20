namespace Engage.Application.Services.EmployeeTransactions.Commands;

public class UpdateEmployeeTransactionStatusCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int EmployeeTransactionStatusId { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class UpdateEmployeeTransactionStatusCommandHandler : IRequestHandler<UpdateEmployeeTransactionStatusCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _user;

    public UpdateEmployeeTransactionStatusCommandHandler(IAppDbContext context, IMediator mediator, IUserService user)
    {
        _context = context;
        _mediator = mediator;
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateEmployeeTransactionStatusCommand command, CancellationToken cancellationToken)
    {
        var employeeTransaction = await _context.EmployeeTransactions
                                                    .Include(x => x.EmployeeTransactionType)
                                                    .Include(x => x.EmployeeRecurringTransaction)
                                                    .SingleAsync(x => x.EmployeeTransactionId == command.Id, cancellationToken);


        employeeTransaction.EmployeeTransactionStatusId = command.EmployeeTransactionStatusId;

        if (command.EmployeeTransactionStatusId == (int)EmployeeTransactionStatusId.Approved)
        {
            employeeTransaction.ApprovedDate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(_user.UserName))
            {
                employeeTransaction.ApprovedBy = _user.UserName;
            }
        }

        if (command.EmployeeTransactionStatusId == (int)EmployeeTransactionStatusId.Rejected)
        {
            employeeTransaction.RejectedDate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(_user.UserName))
            {
                employeeTransaction.RejectedBy = _user.UserName;
            }
        }

        if (employeeTransaction.EmployeeTransactionType.IsRecurring)
        {
            employeeTransaction.EmployeeRecurringTransaction.EmployeeRecurringTransactionStatusId = command.EmployeeTransactionStatusId;
            if (command.EmployeeTransactionStatusId == (int)EmployeeTransactionStatusId.Approved)
            {
                employeeTransaction.EmployeeRecurringTransaction.ApprovedDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(_user.UserName))
                {
                    employeeTransaction.EmployeeRecurringTransaction.ApprovedBy = _user.UserName;
                }
            }

            if (command.EmployeeTransactionStatusId == (int)EmployeeTransactionStatusId.Rejected)
            {
                employeeTransaction.EmployeeRecurringTransaction.RejectedDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(_user.UserName))
                {
                    employeeTransaction.EmployeeRecurringTransaction.RejectedBy = _user.UserName;
                }
            }
        }

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = command.Id;
            return opStatus;
        }

        return new OperationStatus(status: true);
    }
}
