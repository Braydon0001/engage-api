namespace Engage.Application.Services.StoreAssetTypes.Queries;

public class StoreAssetTypeOptionQuery : IRequest<List<StoreAssetTypeOption>>
{
}
public record StoreAssetTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetTypeOptionQuery, List<StoreAssetTypeOption>>
{
    public async Task<List<StoreAssetTypeOption>> Handle(StoreAssetTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetTypes.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.Disabled == false);

        var entities = await queryable.OrderBy(e => e.Id)
                                      .ProjectTo<StoreAssetTypeOption>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return entities;
    }
}