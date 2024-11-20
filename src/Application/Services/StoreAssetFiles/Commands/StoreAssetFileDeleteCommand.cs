namespace Engage.Application.Services.StoreAssetFiles.Commands;

public class StoreAssetFileDeleteCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class StoreAssetFileDeleteHandler : FileDeleteHandler, IRequestHandler<StoreAssetFileDeleteCommand, bool>
{
    public StoreAssetFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(StoreAssetFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreAssetFiles.SingleOrDefaultAsync(e => e.StoreAssetFileId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null)
        {
            return false;
        }

        var fileCommand = new FileDeleteCommand
        {
            Id = command.Id,
            FileName = entity.Files[0].Name,
            FileType = entity.Files[0].Type
        };

        if (entity.StoreAssetFileTypeId == (int)StoreAssetFileTypeId.AssetContract)
        {
            var assetFiles = await _context.StoreAssetFiles.Where(e => e.StoreAssetId == entity.StoreAssetFileId
                                                                    && e.StoreAssetFileId != entity.StoreAssetFileId)
                                                           .ToListAsync(cancellationToken);
            if (!assetFiles.Any()
                || assetFiles.Where(e => e.StoreAssetFileTypeId == (int)StoreAssetFileTypeId.AssetContract).ToList().Count == 0)
            {
                var asset = await _context.StoreAssets.SingleOrDefaultAsync(e => e.StoreAssetId == entity.StoreAssetId, cancellationToken);
                asset.HasContract = false;
            }
        }

        await _file.DeleteAsync(fileCommand, nameof(StoreAssetFile), cancellationToken);

        _context.StoreAssetFiles.Remove(entity);

        //entity.Deleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class StoreAssetFileDeleteValidator : AbstractValidator<StoreAssetFileDeleteCommand>
{
    public StoreAssetFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}