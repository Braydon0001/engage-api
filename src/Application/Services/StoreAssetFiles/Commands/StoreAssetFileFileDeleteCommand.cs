namespace Engage.Application.Services.StoreAssetFiles.Commands;

public class StoreAssetFileFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record StoreAssetFileFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<StoreAssetFileFileDeleteCommand, bool>
{
    public async Task<bool> Handle(StoreAssetFileFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var fileType = await Context.StoreAssetFileTypes.SingleOrDefaultAsync(e => e.Name.ToLower() == command.FileType.ToLower(), cancellationToken);
        var entity = await Context.StoreAssetFiles.FirstOrDefaultAsync(e => e.StoreAssetId == command.Id && e.StoreAssetFileTypeId == fileType.StoreAssetFileTypeId, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        // commented out for soft delete
        await File.DeleteAsync(command, nameof(StoreAssetFile), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        Context.StoreAssetFiles.Remove(entity);

        //entity.Deleted = true;

        await Context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class StoreAssetFileFileDeleteValidator : FileDeleteValidator<StoreAssetFileFileDeleteCommand>
{
    public StoreAssetFileFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}