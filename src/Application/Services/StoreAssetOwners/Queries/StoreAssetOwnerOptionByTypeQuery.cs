namespace Engage.Application.Services.StoreAssetOwners.Queries;

public class StoreAssetOwnerOptionByTypeQuery : IRequest<List<StoreAssetOwnerOption>>
{
    public int StoreAssetTypeId { get; set; }
}
public record StoreAssetOwnerOptionByTypeHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetOwnerOptionByTypeQuery, List<StoreAssetOwnerOption>>
{
    public async Task<List<StoreAssetOwnerOption>> Handle(StoreAssetOwnerOptionByTypeQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetOwners.AsQueryable().AsNoTracking();

        if (query.StoreAssetTypeId > 0)
        {
            var assetOwnerIds = await Context.StoreAssetOwnerStoreAssetTypes.AsNoTracking()
                                             .Where(e => e.StoreAssetTypeId == query.StoreAssetTypeId)
                                             .Select(e => e.StoreAssetOwnerId)
                                             .ToListAsync();
            queryable = queryable.Where(e => assetOwnerIds.Contains(e.Id));
        }

        return await queryable.OrderBy(e => e.Id)
                                .ProjectTo<StoreAssetOwnerOption>(Mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken);
    }
}
