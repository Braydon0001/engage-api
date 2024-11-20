namespace Engage.Application.Services.StoreAssetFiles.Commands;

public class StoreAssetFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
    public int StoreAssetId { get; set; }
}
public class StoreAssetFileUploadHandler : FileUploadHandler, IRequestHandler<StoreAssetFileUploadCommand, JsonFile>
{
    private readonly IMediator _mediator;
    public StoreAssetFileUploadHandler(IAppDbContext context, IFileService file, IMediator mediator) : base(context, file)
    {
        _mediator = mediator;
    }

    public async Task<JsonFile> Handle(StoreAssetFileUploadCommand command, CancellationToken cancellationToken)
    {
        var asset = await _context.StoreAssets.SingleOrDefaultAsync(e => e.StoreAssetId == command.StoreAssetId, cancellationToken);
        var assetType = await _context.StoreAssetFileTypes.SingleOrDefaultAsync(e => e.Name.ToLower() == command.FileType.ToLower(), cancellationToken);
        if (asset == null)
            return null;
        if (assetType == null)
            return null;

        var entity = await _mediator.Send(new StoreAssetFileInsertCommand
        {
            StoreAssetId = asset.StoreAssetId,
            StoreAssetFileTypeId = assetType.StoreAssetFileTypeId,
        }, cancellationToken);

        var options = new FileUploadOptions
        {
            ContainerName = nameof(StoreAssetFile),
            EntityFiles = entity.Files,
            MaxFiles = 100,
            OverwriteType = false
        };

        command.Id = entity.StoreAssetFileId;
        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }
}