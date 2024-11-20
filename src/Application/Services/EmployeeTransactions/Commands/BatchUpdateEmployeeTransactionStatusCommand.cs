namespace Engage.Application.Services.EmployeeTransactions.Commands;

public class BatchUpdateEmployeeTransactionStatusCommand : IRequest<OperationStatus>
{
    public List<int> EmployeeTransactionIds { get; set; }
    public int EmployeeTransactionStatusId { get; set; }
}

public class BatchUpdateEmployeeTransactionStatusCommandHandler : IRequestHandler<BatchUpdateEmployeeTransactionStatusCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public BatchUpdateEmployeeTransactionStatusCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(BatchUpdateEmployeeTransactionStatusCommand command, CancellationToken cancellationToken)
    {
        foreach (var id in command.EmployeeTransactionIds)
        {
            var updateStatusCommand = new UpdateEmployeeTransactionStatusCommand
            {
                Id = id,
                EmployeeTransactionStatusId = command.EmployeeTransactionStatusId,
                SaveChanges = false,
            };

            await _mediator.Send(updateStatusCommand, cancellationToken);
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
