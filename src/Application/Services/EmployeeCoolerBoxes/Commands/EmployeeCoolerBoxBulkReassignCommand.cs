namespace Engage.Application.Services.EmployeeCoolerBoxes.Commands;

public class EmployeeCoolerBoxBulkReassignCommand : IRequest<OperationStatus>
{
    public List<int> EmployeeCoolerBoxIds { get; set; }
    public int EmployeeId { get; set; }
}

public class EmployeeCoolerBoxBulkReassignHandler : IRequestHandler<EmployeeCoolerBoxBulkReassignCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public EmployeeCoolerBoxBulkReassignHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(EmployeeCoolerBoxBulkReassignCommand request, CancellationToken cancellationToken)
    {
        foreach (var id in request.EmployeeCoolerBoxIds)
        {
            await _mediator.Send(new EmployeeCoolerBoxReassignCommand
            {
                EmployeeCoolerBoxId = id,
                EmployeeId = request.EmployeeId,
                SaveChanges = false
            }, cancellationToken);
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }
}

public class EmployeeCoolerBoxBulkReassignValidator<T> : AbstractValidator<EmployeeCoolerBoxBulkReassignCommand>
{
    public EmployeeCoolerBoxBulkReassignValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        RuleForEach(x => x.EmployeeCoolerBoxIds).GreaterThan(0).NotEmpty();
    }
}