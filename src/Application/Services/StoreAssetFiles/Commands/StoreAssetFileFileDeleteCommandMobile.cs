namespace Engage.Application.Services.StoreAssetFiles.Commands;

public class StoreAssetFileFileDeleteCommandMobile : FileDeleteCommand, IRequest<bool>
{
}

public record StoreAssetFileFileDeleteMobileHandler(IAppDbContext Context, IFileService File) : IRequestHandler<StoreAssetFileFileDeleteCommandMobile, bool>
{
    public async Task<bool> Handle(StoreAssetFileFileDeleteCommandMobile command, CancellationToken cancellationToken)
    {


        var fileType = await Context.StoreAssetFileTypes.SingleOrDefaultAsync(e => e.Name.ToLower() == command.FileType.ToLower(), cancellationToken);
        var selectedAssetFile = await Context.StoreAssetFiles.Where(e => e.StoreAssetFileTypeId == fileType.StoreAssetFileTypeId
        && e.StoreAssetId == command.Id)
            .ToListAsync(cancellationToken);

        foreach (var assetFile in selectedAssetFile)
        {
            if (assetFile.Files.FileExists(command))
            {
                var fileCommand = new FileDeleteCommand
                {
                    Id = assetFile.StoreAssetFileId,
                    FileName = assetFile.Files[0].Name,
                    FileType = assetFile.Files[0].Type
                };

                //if (assetFile.StoreAssetFileTypeId == (int)StoreAssetFileTypeId.AssetContract)
                //{
                //    var assetFiles = await Context.StoreAssetFiles.Where(e => e.StoreAssetId == assetFile.StoreAssetFileId
                //                                                            && e.StoreAssetFileId != assetFile.StoreAssetFileId)
                //                                                   .ToListAsync(cancellationToken);
                //    if (!assetFiles.Any()
                //        || assetFiles.Where(e => e.StoreAssetFileTypeId == (int)StoreAssetFileTypeId.AssetContract).ToList().Count == 0)
                //    {
                //        var asset = await Context.StoreAssets.SingleOrDefaultAsync(e => e.StoreAssetId == assetFile.StoreAssetId, cancellationToken);
                //        asset.HasContract = false;
                //    }
                //}

                await File.DeleteAsync(fileCommand, nameof(StoreAssetFile), cancellationToken);

                Context.StoreAssetFiles.Remove(assetFile);

                await Context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
        return false;

    }
}

public class StoreAssetFileFileDeleteMobileValidator : FileDeleteValidator<StoreAssetFileFileDeleteCommandMobile>
{
    public StoreAssetFileFileDeleteMobileValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}