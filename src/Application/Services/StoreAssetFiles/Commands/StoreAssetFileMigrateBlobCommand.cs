namespace Engage.Application.Services.StoreAssetFiles.Commands;

public class StoreAssetFileMigrateBlobCommand : IRequest<OperationStatus>
{

}
public record StoreAssetFileMigrateBlobHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetFileMigrateBlobCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(StoreAssetFileMigrateBlobCommand command, CancellationToken cancellationToken)
    {
        var blobs = await Context.StoreAssetBlobs.ToListAsync(cancellationToken);

        foreach (var blob in blobs)
        {
            Uri uri = new Uri(blob.ImageUrl);
            var filename = System.IO.Path.GetFileName(uri.LocalPath);
            var imageFile = new JsonFile
            {
                Type = "assetImage",
                Url = blob.ImageUrl,
                Name = filename
            };
            Context.StoreAssetFiles.Add(new StoreAssetFile
            {
                StoreAssetId = blob.StoreAssetId,
                StoreAssetFileTypeId = 2,
                Files = new List<JsonFile> { imageFile }
            });
        }

        //var opStatus = await Context.SaveChangesAsync(cancellationToken);
        //return opStatus;
        return new(true);
    }
}