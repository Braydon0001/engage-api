namespace Engage.Application.Services.StoreFilters.Commands;

public class RemoveStoreFilterCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class RemoveStoreFilterCommandHandler : IRequestHandler<RemoveStoreFilterCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public RemoveStoreFilterCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(RemoveStoreFilterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreFilters.SingleAsync(e => e.StoreFilterId == request.Id, cancellationToken);

        _context.StoreFilters.Remove(entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = request.Id;
        return operationStatus;
    }
}
