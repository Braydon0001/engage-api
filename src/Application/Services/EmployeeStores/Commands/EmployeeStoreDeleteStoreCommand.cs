namespace Engage.Application.Services.EmployeeStores.Commands;

public class EmployeeStoreDeleteStoreCommand : IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
}

public class EmployeeStoreDeleteStoreHandler : IRequestHandler<EmployeeStoreDeleteStoreCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public EmployeeStoreDeleteStoreHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(EmployeeStoreDeleteStoreCommand request, CancellationToken cancellationToken)
    {
        var entities = await _context.EmployeeStores.IgnoreQueryFilters()
                                                    .Where(e => e.EmployeeId == request.EmployeeId &&
                                                                e.StoreId == request.StoreId)
                                                    .ToListAsync(cancellationToken);

        _context.EmployeeStores.RemoveRange(entities);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}

public class EmployeeStoreDeleteStoreValidator : AbstractValidator<EmployeeStoreDeleteStoreCommand>
{
    public EmployeeStoreDeleteStoreValidator()
    {
        RuleFor(e => e.EmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(e => e.StoreId).GreaterThan(0).NotEmpty();
    }
}

