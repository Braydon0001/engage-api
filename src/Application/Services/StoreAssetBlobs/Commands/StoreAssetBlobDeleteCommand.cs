namespace Engage.Application.Services.AssetImages.Commands;

public class StoreAssetBlobDeleteCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class StoreAssetBlobDeleteHandler : IRequestHandler<StoreAssetBlobDeleteCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IBlobService _blobService;
    private readonly AzureBlobOptions _options;

    public StoreAssetBlobDeleteHandler(IAppDbContext context, IBlobService blobService, IOptions<AzureBlobOptions> options)
    {
        _context = context;
        _blobService = blobService;
        _options = options.Value;
    }

    public async Task<OperationStatus> Handle(StoreAssetBlobDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreAssetBlobs.SingleOrDefaultAsync(e => e.StoreAssetBlobId == request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(StoreAssetBlob), request.Id);
        }

        var indexOf = !string.IsNullOrWhiteSpace(entity.ImageUrl)
            ? entity.ImageUrl.LastIndexOf("/") + 1
            : -1;

        if (indexOf < entity.ImageUrl.Length)
        {
            var fileName = entity.ImageUrl[indexOf..];
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                await _blobService.DeleteAsync(_options.AssetContainerName, fileName, cancellationToken);
            }
        }

        _context.StoreAssetBlobs.Remove(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = request.Id;
        opStatus.ReturnObject = entity;
        return opStatus;

    }
}
