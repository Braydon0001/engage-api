namespace Engage.Application.Services.StoreAssets.Commands;

public class StoreAssetFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class StoreAssetFileDeleteHandler : FileDeleteHandler, IRequestHandler<StoreAssetFileDeleteCommand, bool>
{
    public StoreAssetFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(StoreAssetFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreAssets.SingleOrDefaultAsync(e => e.StoreAssetId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(StoreAsset), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class StoreAssetFileDeleteValidator : FileDeleteValidator<StoreAssetFileDeleteCommand>
{
    public StoreAssetFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}