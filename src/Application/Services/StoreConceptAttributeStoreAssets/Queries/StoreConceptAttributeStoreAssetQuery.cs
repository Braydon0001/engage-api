using Engage.Application.Services.StoreConceptAttributeStoreAssets.Models;

namespace Engage.Application.Services.StoreConceptAttributeStoreAssets.Queries;

public class StoreConceptAttributeStoreAssetQuery : GetQuery, IRequest<ListResult<StoreConceptAttributeStoreAssetDto>>
{
    public int? StoreConceptAttributeId { get; set; }
    public int? StoreAssetId { get; set; }
    public int? StoreId { get; set; }
}

public class StoreConceptAttributeStoreAssetQueryHandler : BaseQueryHandler, IRequestHandler<StoreConceptAttributeStoreAssetQuery, ListResult<StoreConceptAttributeStoreAssetDto>>
{
    public StoreConceptAttributeStoreAssetQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<StoreConceptAttributeStoreAssetDto>> Handle(StoreConceptAttributeStoreAssetQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreConceptAttributeStoreAssets.AsQueryable();

        if (request.StoreConceptAttributeId.HasValue && !request.StoreAssetId.HasValue && !request.StoreId.HasValue)
        {
            queryable = queryable.Where(x => x.StoreConceptAttributeId == request.StoreConceptAttributeId);
        }

        if (!request.StoreConceptAttributeId.HasValue && request.StoreAssetId.HasValue && !request.StoreId.HasValue)
        {
            queryable = queryable.Where(x => x.StoreAssetId == request.StoreAssetId);
        }

        if (request.StoreConceptAttributeId.HasValue && request.StoreAssetId.HasValue && !request.StoreId.HasValue)
        {
            queryable = queryable.Where(x => x.StoreAssetId == request.StoreAssetId && x.StoreConceptAttributeId == request.StoreConceptAttributeId);
        }

        if (request.StoreId.HasValue && !request.StoreConceptAttributeId.HasValue && !request.StoreAssetId.HasValue)
        {
            queryable = queryable.Where(x => x.StoreAsset.StoreId == request.StoreId);
        }

        if (request.StoreId.HasValue && request.StoreConceptAttributeId.HasValue && !request.StoreAssetId.HasValue)
        {
            queryable = queryable.Where(x => x.StoreConceptAttributeId == request.StoreConceptAttributeId && x.StoreAsset.StoreId == request.StoreId);
        }

        if (request.StoreId.HasValue && !request.StoreConceptAttributeId.HasValue && request.StoreAssetId.HasValue)
        {
            queryable = queryable.Where(x => x.StoreAsset.StoreId == request.StoreId && x.StoreAsset.StoreAssetId == request.StoreAssetId);
        }

        var entities = await queryable.ProjectTo<StoreConceptAttributeStoreAssetDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        if (entities.Count == 0)
        {
            return null;
        }

        return new ListResult<StoreConceptAttributeStoreAssetDto>(entities);
    }
}