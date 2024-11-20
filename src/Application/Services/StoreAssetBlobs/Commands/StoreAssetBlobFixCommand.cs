namespace Engage.Application.Services.AssetImages.Commands;

public class StoreAssetBlobFixCommand : StoreAssetBlobCommand, IRequest<OperationStatus>
{
}

public class StoreAssetBlobFixHandler : BaseUpdateCommandHandler, IRequestHandler<StoreAssetBlobFixCommand, OperationStatus>
{
    public StoreAssetBlobFixHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(StoreAssetBlobFixCommand command, CancellationToken cancellationToken)
    {
        var assetBlobs = await _context.StoreAssetBlobs.Where(
                                                      x => x.ImageUrl.Contains("https://ensource.insightconsulting.co.za/surveyphotos/"))
                                                   .ToListAsync(cancellationToken);

        if (assetBlobs.Any())
        {
            foreach (var assetBlob in assetBlobs)
            {
                assetBlob.ImageUrl = assetBlob.ImageUrl.Replace("https://ensource.insightconsulting.co.za/surveyphotos/", "https://ensource.insightconsulting.co.za/wwwroot/surveyphotos/");
            }
        }

        //await _context.SaveChangesAsync(cancellationToken);
        return new OperationStatus { Status = true };
    }
}
