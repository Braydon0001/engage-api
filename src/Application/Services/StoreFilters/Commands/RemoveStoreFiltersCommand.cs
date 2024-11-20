namespace Engage.Application.Services.StoreFilters.Commands;

public class RemoveStoreFiltersCommand : IRequest<OperationStatus>
{
    public string Filter { get; set; }
}

public class RemoveStoreFiltersCommandHandler : IRequestHandler<RemoveStoreFiltersCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public RemoveStoreFiltersCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(RemoveStoreFiltersCommand request, CancellationToken cancellationToken)
    {
        var entities = await _context.StoreFilters.Where(e => e.Filter == request.Filter)
                                                  .ToListAsync(cancellationToken);

        _context.StoreFilters.RemoveRange(entities);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}