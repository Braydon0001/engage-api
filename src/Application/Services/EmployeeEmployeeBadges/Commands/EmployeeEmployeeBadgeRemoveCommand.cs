namespace Engage.Application.Services.EmployeeEmployeeBadges.Commands;

public class EmployeeEmployeeBadgeRemoveCommand : IRequest<OperationStatus>
{
    public int EmployeeBadgeId { get; set; }
    public int EmployeeId { get; set; }

}

public class EmployeeEmployeeBadgeRemoveCommandHandler : IRequestHandler<EmployeeEmployeeBadgeRemoveCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;


    public EmployeeEmployeeBadgeRemoveCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(EmployeeEmployeeBadgeRemoveCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeEmployeeBadges.Where(x => x.EmployeeId == request.EmployeeId && x.EmployeeBadgeId == request.EmployeeBadgeId).SingleAsync();
        
        if (entity != null)
        {
            _context.EmployeeEmployeeBadges.Remove(entity);
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            return opStatus;
        }

        throw new Exception("Cannot Delete While Value Is In Use");
    }
}
