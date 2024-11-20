namespace Engage.Application.Services.EmployeeStores.Commands;

public class EmployeeStoreDeleteEngageDepartmentCommand : IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
    public int EngageDepartmentId { get; set; }
}

public class EmployeeStoreDeleteEngageDepartmentHandler : IRequestHandler<EmployeeStoreDeleteEngageDepartmentCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public EmployeeStoreDeleteEngageDepartmentHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(EmployeeStoreDeleteEngageDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmployeeStores.IgnoreQueryFilters()
                                                    .Where(e => e.EmployeeId == request.EmployeeId &&
                                                                e.EngageSubGroup.EngageDepartmentId == request.EngageDepartmentId)
                                                    .ToListAsync(cancellationToken);

        _context.EmployeeStores.RemoveRange(entities);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}

public class EmployeeStoreDeleteEngageDepartmentValidator : AbstractValidator<EmployeeStoreDeleteEngageDepartmentCommand>
{
    public EmployeeStoreDeleteEngageDepartmentValidator()
    {
        RuleFor(e => e.EmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(e => e.EngageDepartmentId).GreaterThan(0).NotEmpty();
    }
}

