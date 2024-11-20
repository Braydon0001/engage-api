namespace Engage.Application.Services.AssetImages.Commands;

public class StoreAssetBlobUploadCommand : IRequest<OperationStatus>
{
    public int StoreAssetId { get; set; }
    public IFormFile File { get; set; }
}

public class StoreAssetBlobUploadHandler : BaseCreateCommandHandler, IRequestHandler<StoreAssetBlobUploadCommand, OperationStatus>
{
    private readonly AzureBlobOptions _options;
    private readonly IBlobService _imageService;

    public StoreAssetBlobUploadHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IBlobService imageService, IOptions<AzureBlobOptions> options)
        : base(context, mapper, mediator)
    {
        _imageService = imageService;
        _options = options.Value;
    }

    public async Task<OperationStatus> Handle(StoreAssetBlobUploadCommand request, CancellationToken cancellationToken)
    {
        request.File.ThrowIfNull(nameof(request.File));

        // Prepend this prefix `id{id}.` to the file name.
        var fileName = $"id{request.StoreAssetId}-{request.File.FileName}";

        Uri uri = null;
        try
        {
            uri = await _imageService.UploadAsync(request.File.OpenReadStream(), _options.AssetContainerName, fileName, cancellationToken);
        }
        catch (Exception ex)
        {
            return OperationStatus.CreateFromException("Error uploading image", ex);
        }

        var entity = await _context.StoreAssetBlobs.SingleOrDefaultAsync(e => e.StoreAssetId == request.StoreAssetId &&
                                                                              e.ImageUrl == uri.AbsoluteUri, cancellationToken);
        if (entity != null)
        {
            return new OperationStatus
            {
                Status = true,
                OperationId = entity.StoreAssetBlobId
            };
        }

        return await _mediator.Send(new StoreAssetBlobCreateCommand
        {
            StoreAssetId = request.StoreAssetId,
            ImageUrl = uri.AbsoluteUri
        }, cancellationToken);
    }
}
