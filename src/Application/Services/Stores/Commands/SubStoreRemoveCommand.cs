namespace Engage.Application.Services.Stores.Commands;

public class SubStoreRemoveCommand : IRequest<OperationStatus>
{
    public int StoreId { get; set; }
    public int ChildStoreId { get; set; }
}

public class SubStoreRemoveHandler : IRequestHandler<SubStoreRemoveCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public SubStoreRemoveHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(SubStoreRemoveCommand query, CancellationToken cancellationToken)
    {
        var store = await _context.Stores.SingleAsync(e => e.StoreId == query.ChildStoreId, cancellationToken);
        store.ParentStoreId = null;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = query.StoreId;
        return operationStatus;
    }
}

public class SubStoreRemoveValidator : AbstractValidator<SubStoreRemoveCommand>
{
    public SubStoreRemoveValidator()
    {
        RuleFor(x => x.StoreId).NotEmpty();
        RuleFor(x => x.ChildStoreId).NotEmpty();
    }
}