namespace Engage.Application.Services.Stores.Commands;

public class SubStoreAddCommand : IRequest<OperationStatus>
{
    public int StoreId { get; set; }
    public int ChildStoreId { get; set; }
}

public class SubStoreAddHandler : IRequestHandler<SubStoreAddCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public SubStoreAddHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(SubStoreAddCommand query, CancellationToken cancellationToken)
    {
        if (query.StoreId == query.ChildStoreId)
        {
            throw new Exception("A store can't be a sub store of itself");
        }

        var store = await _context.Stores.SingleAsync(e => e.StoreId == query.ChildStoreId, cancellationToken);
        if (store.ParentStoreId == query.StoreId)
        {
            throw new Exception("This store is already a sub store ");
        }
        store.ParentStoreId = query.StoreId;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = query.StoreId;
        return operationStatus;
    }
}

public class SubStoreAddValidator : AbstractValidator<SubStoreAddCommand>
{
    public SubStoreAddValidator()
    {
        RuleFor(x => x.StoreId).NotEmpty();
        RuleFor(x => x.ChildStoreId).NotEmpty();
    }
}