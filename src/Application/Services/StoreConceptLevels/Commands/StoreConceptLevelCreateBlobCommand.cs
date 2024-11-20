namespace Engage.Application.Services.StoreConceptLevels.Commands;

public class StoreConceptLevelCreateBlobCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public IFormFile File { get; set; }
}

public class StoreConceptLevelCreateBlobHandler : IRequestHandler<StoreConceptLevelCreateBlobCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IBlobService _blob;

    public StoreConceptLevelCreateBlobHandler(IAppDbContext context, IMediator mediator, IBlobService blob)
    {
        _context = context;
        _mediator = mediator;
        _blob = blob;
    }

    public async Task<OperationStatus> Handle(StoreConceptLevelCreateBlobCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreConceptLevels.SingleOrDefaultAsync(e => e.StoreConceptLevelId == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(StoreConceptLevel), request.Id);
        }

        var file = request.File;
        var fileName = _blob.CreateFileName(request.Id, file.FileName);
        var uri = await _blob.UploadAsync(file.OpenReadStream(), "storeconcept", fileName, cancellationToken);
        entity.BlobUrl = uri.AbsoluteUri;
        entity.BlobName = fileName;

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreConceptLevelId;
        return opStatus; ;
    }
}