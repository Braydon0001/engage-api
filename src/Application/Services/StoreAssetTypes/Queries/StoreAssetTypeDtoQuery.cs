namespace Engage.Application.Services.StoreAssetTypes.Queries;

public class StoreAssetTypeDtoQuery : IRequest<List<StoreAssetTypeDto>>
{
}
public record StoreAssetTypeDtoHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetTypeDtoQuery, List<StoreAssetTypeDto>>
{
    public async Task<List<StoreAssetTypeDto>> Handle(StoreAssetTypeDtoQuery request, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetTypes.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.AssetOwnerAssetTypes)
                                 .ThenInclude(e => e.StoreAssetOwner)
                             .Include(e => e.AssetSubTypes)
                                 .ThenInclude(e => e.StoreAssetSubType);

        return await queryable.OrderBy(e => e.Id)
                              .ProjectTo<StoreAssetTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}