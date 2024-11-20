namespace Engage.Application.Services.StoreAssetTypeContacts.Queries;

public class StoreAssetTypeContactOptionQuery : IRequest<List<StoreAssetTypeContactOption>>
{
    public int StoreAssetTypeId { get; set; }
}

public record StoreAssetTypeContactOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetTypeContactOptionQuery, List<StoreAssetTypeContactOption>>
{
    public async Task<List<StoreAssetTypeContactOption>> Handle(StoreAssetTypeContactOptionQuery query, CancellationToken cancellationToken)
    {

        var queryable = Context.StoreAssetTypeContacts.AsQueryable().AsNoTracking();

        if (query.StoreAssetTypeId > 0)
        {
            var storeAssetTypeContactIds = await Context.StoreAssetTypeStoreAssetTypeContacts
                                                    .AsNoTracking()
                                                    .Where(e => e.StoreAssetTypeId == query.StoreAssetTypeId)
                                                    .Select(e => e.StoreAssetTypeContactId)
                                                    .ToListAsync(cancellationToken);

            queryable = queryable.Where(e => storeAssetTypeContactIds.Contains(e.StoreAssetTypeContactId));
        }

        return await queryable.OrderBy(e => e.StoreAssetTypeContactId)
                              .ProjectTo<StoreAssetTypeContactOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}