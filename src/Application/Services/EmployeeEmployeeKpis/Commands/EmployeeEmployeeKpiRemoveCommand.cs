namespace Engage.Application.Services.EmployeeEmployeeKpis.Commands;

public class EmployeeEmployeeKpiRemoveCommand : IRequest<OperationStatus>
{
    public int EmployeeKpiId { get; set; }
    public int EmployeeId { get; set; }

}

public class EmployeeEmployeeKpiRemoveCommandHandler : IRequestHandler<EmployeeEmployeeKpiRemoveCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;


    public EmployeeEmployeeKpiRemoveCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(EmployeeEmployeeKpiRemoveCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeEmployeeKpis.Where(x => x.EmployeeId == request.EmployeeId && x.EmployeeKpiId == request.EmployeeKpiId).SingleAsync();

        if (entity != null)
        {
            _context.EmployeeEmployeeKpis.Remove(entity);
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            return opStatus;
        }

        throw new Exception("Failed to Delete");
    }
}
