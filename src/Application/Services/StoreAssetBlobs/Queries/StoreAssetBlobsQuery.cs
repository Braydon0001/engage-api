namespace Engage.Application.Services.AssetImages.Queries;

public class StoreAssetBlobsQuery : GetQuery, IRequest<ListResult<EntityBlobDto>>
{
    public int StoreAssetId { get; set; }
}

public class StoreAssetBlobsQueryHandler : IRequestHandler<StoreAssetBlobsQuery, ListResult<EntityBlobDto>>
{
    private readonly IAppDbContext _context;

    public StoreAssetBlobsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ListResult<EntityBlobDto>> Handle(StoreAssetBlobsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.StoreAssetBlobs.Where(e => e.StoreAssetId == request.StoreAssetId)
                                                     .OrderByDescending(e => e.StoreAssetBlobId)
                                                     .Select(e => new EntityBlobDto(e))
                                                     .ToListAsync(cancellationToken);
        return new ListResult<EntityBlobDto>(entities);
    }
}

