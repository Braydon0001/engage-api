namespace Engage.Application.Services.StoreConceptLevels.Commands;

public class StoreConceptLevelRemoveCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class StoreConceptLevelRemoveCommandHandler : IRequestHandler<StoreConceptLevelRemoveCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IBlobService _blob;

    public StoreConceptLevelRemoveCommandHandler(IAppDbContext context, IBlobService blob)
    {
        _context = context;
        _blob = blob;
    }

    public async Task<OperationStatus> Handle(StoreConceptLevelRemoveCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreConceptLevels.SingleAsync(e => e.StoreConceptLevelId == request.Id, cancellationToken);

        if (!string.IsNullOrWhiteSpace(entity.BlobName))
        {
            await _blob.DeleteAsync("storeconcept", entity.BlobName, cancellationToken);
        }

        _context.StoreConceptLevels.Remove(entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = request.Id;
        return operationStatus;
    }
}
