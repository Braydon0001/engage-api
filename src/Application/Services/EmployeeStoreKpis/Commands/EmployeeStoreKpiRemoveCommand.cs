namespace Engage.Application.Services.EmployeeStoreKpis.Commands;

public class EmployeeStoreKpiRemoveCommand : IRequest<OperationStatus>
{
    public int EmployeeKpiId { get; set; }
    public int EmployeeId { get; set; }

}

public class EmployeeStoreKpiRemoveCommandHandler : IRequestHandler<EmployeeStoreKpiRemoveCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;


    public EmployeeStoreKpiRemoveCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(EmployeeStoreKpiRemoveCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStoreKpis.Where(x => x.EmployeeId == request.EmployeeId && x.EmployeeKpiId == request.EmployeeKpiId).SingleAsync();

        if (entity != null)
        {
            _context.EmployeeStoreKpis.Remove(entity);
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            return opStatus;
        }

        throw new Exception("Cannot Delete While Value Is In Use");
    }
}
