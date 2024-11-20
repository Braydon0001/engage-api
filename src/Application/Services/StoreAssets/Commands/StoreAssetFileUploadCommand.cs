namespace Engage.Application.Services.StoreAssets.Commands;

public class StoreAssetFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public class StoreAssetFileUploadHandler : FileUploadHandler, IRequestHandler<StoreAssetFileUploadCommand, JsonFile>
{
    public StoreAssetFileUploadHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<JsonFile> Handle(StoreAssetFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreAssets.SingleOrDefaultAsync(e => e.StoreAssetId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(StoreAsset),
            EntityFiles = entity.Files,
            MaxFiles = 6,
            OverwriteType = false
        };
        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class StoreAssetFileUploadValidtor : FileUploadValidator<StoreAssetFileUploadCommand>
{
    public StoreAssetFileUploadValidtor()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}