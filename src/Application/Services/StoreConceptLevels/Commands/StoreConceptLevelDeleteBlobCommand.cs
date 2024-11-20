namespace Engage.Application.Services.StoreConceptLevels.Commands;

public class StoreConceptLevelDeleteBlobCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class StoreConceptLevelDeleteBlobCommandHandler : IRequestHandler<StoreConceptLevelDeleteBlobCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IBlobService _blob;

    public StoreConceptLevelDeleteBlobCommandHandler(IAppDbContext context, IMediator mediator, IBlobService blob)
    {
        _context = context;
        _mediator = mediator;
        _blob = blob;
    }

    public async Task<OperationStatus> Handle(StoreConceptLevelDeleteBlobCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreConceptLevels.SingleOrDefaultAsync(e => e.StoreConceptLevelId == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(StoreConceptLevel), request.Id);
        }

        if (!string.IsNullOrWhiteSpace(entity.BlobName))
        {
            await _blob.DeleteAsync("storeconcept", entity.BlobName, cancellationToken);
            entity.BlobUrl = string.Empty;
            entity.BlobName = string.Empty;

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.StoreConceptLevelId;
            return opStatus;
        }

        return new OperationStatus(true);
    }
}