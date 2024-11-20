namespace Engage.Application.Services.EmployeeVehicles.Commands;

public class EmployeeVehicleBulkReassignCommand : IRequest<OperationStatus>
{
    public List<int> EmployeeVehicleIds { get; set; }
    public int EmployeeId { get; set; }
}

public class EmployeeVehicleBulkReassignHandler : IRequestHandler<EmployeeVehicleBulkReassignCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public EmployeeVehicleBulkReassignHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(EmployeeVehicleBulkReassignCommand request, CancellationToken cancellationToken)
    {
        foreach (var id in request.EmployeeVehicleIds)
        {
            await _mediator.Send(new EmployeeVehicleReassignCommand
            {
                EmployeeVehicleId = id,
                EmployeeId = request.EmployeeId,
                SaveChanges = false
            }, cancellationToken);
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }
}

public class EmployeeVehicleBulkReassignValidator<T> : AbstractValidator<EmployeeVehicleBulkReassignCommand>
{
    public EmployeeVehicleBulkReassignValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        RuleForEach(x => x.EmployeeVehicleIds).GreaterThan(0).NotEmpty();
    }
}