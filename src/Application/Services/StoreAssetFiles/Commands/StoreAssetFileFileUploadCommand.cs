namespace Engage.Application.Services.StoreAssetFiles.Commands;

public class StoreAssetFileFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
    public int StoreAssetId { get; set; }
}

public record StoreAssetFileFileUploadHandler(IAppDbContext Context, IFileService File, IMediator Mediator) : IRequestHandler<StoreAssetFileFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(StoreAssetFileFileUploadCommand command, CancellationToken cancellationToken)
    {
        var asset = await Context.StoreAssets.SingleOrDefaultAsync(e => e.StoreAssetId == command.StoreAssetId, cancellationToken);
        var assetFileType = await Context.StoreAssetFileTypes.SingleOrDefaultAsync(e => e.Name.ToLower() == command.FileType.ToLower(), cancellationToken);
        if (asset == null)
        {
            return null;
        }
        if (assetFileType == null)
        {
            return null;
        }

        var entity = await Mediator.Send(new StoreAssetFileInsertCommand
        {
            StoreAssetId = asset.StoreAssetId,
            StoreAssetFileTypeId = assetFileType.StoreAssetFileTypeId
        }, cancellationToken);

        var options = new FileUploadOptions
        {
            ContainerName = nameof(StoreAssetFile),
            EntityFiles = entity.Files,
            MaxFiles = 100,
            OverwriteType = false
        };

        command.Id = entity.StoreAssetFileId;
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        if (assetFileType.StoreAssetFileTypeId == (int)StoreAssetFileTypeId.AssetContract)
        {
            asset.HasContract = true;
        }

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class StoreAssetFileFileUploadValidator : FileUploadValidator<StoreAssetFileFileUploadCommand>
{
    public StoreAssetFileFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}