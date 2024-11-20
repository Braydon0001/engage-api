namespace Engage.Application.Services.StoreStoreConcepts.Commands;

public class StoreStoreConceptRemoveCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class StoreStoreConceptRemoveCommandHandler : IRequestHandler<StoreStoreConceptRemoveCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public StoreStoreConceptRemoveCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(StoreStoreConceptRemoveCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreStoreConcepts.SingleAsync(e => e.StoreStoreConceptId == request.Id, cancellationToken);

        _context.StoreStoreConcepts.Remove(entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = request.Id;
        return operationStatus;
    }
}
