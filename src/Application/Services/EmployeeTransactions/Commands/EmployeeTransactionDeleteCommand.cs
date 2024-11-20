namespace Engage.Application.Services.EmployeeTransactions.Commands;

public class EmployeeTransactionDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class EmployeeTransactionDeleteHandler : IRequestHandler<EmployeeTransactionDeleteCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public EmployeeTransactionDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(EmployeeTransactionDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeTransactions.SingleOrDefaultAsync(e => e.EmployeeTransactionId == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(EmployeeTransaction), request.Id);

        //_context.EmployeeTransactions.Remove(entity);
        entity.Deleted = true;

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        return opStatus;
    }
}

public class EmployeeTransactionDeleteValidator : AbstractValidator<EmployeeTransactionDeleteCommand>
{
    public EmployeeTransactionDeleteValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
    }
}

