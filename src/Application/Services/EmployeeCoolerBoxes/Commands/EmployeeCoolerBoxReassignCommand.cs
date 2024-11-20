namespace Engage.Application.Services.EmployeeCoolerBoxes.Commands;

public class EmployeeCoolerBoxReassignCommand : IRequest<OperationStatus>
{
    public int EmployeeCoolerBoxId { get; set; }
    public int EmployeeId { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class EmployeeCoolerBoxReassignHandler : IRequestHandler<EmployeeCoolerBoxReassignCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public EmployeeCoolerBoxReassignHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(EmployeeCoolerBoxReassignCommand request, CancellationToken cancellationToken)
    {
        var CoolerBox = await _context.EmployeeCoolerBoxes.SingleOrDefaultAsync(e => e.EmployeeCoolerBoxId == request.EmployeeCoolerBoxId, cancellationToken);
        if (CoolerBox == null)
        {
            throw new NotFoundException(nameof(EmployeeCoolerBox), request.EmployeeCoolerBoxId);
        }

        if (CoolerBox.EmployeeId == request.EmployeeId)
        {
            return new OperationStatus(true);
        }

        var history = new EmployeeCoolerBoxHistory
        {
            EmployeeCoolerBoxId = CoolerBox.EmployeeCoolerBoxId,
            OldEmployeeId = CoolerBox.EmployeeId,
            NewEmployeeId = request.EmployeeId,
        };

        _context.EmployeeCoolerBoxHistories.Add(history);
        CoolerBox.EmployeeId = request.EmployeeId;

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
