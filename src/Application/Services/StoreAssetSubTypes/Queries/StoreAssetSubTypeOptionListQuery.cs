// auto-generated
namespace Engage.Application.Services.StoreAssetSubTypes.Queries;

public class StoreAssetSubTypeOptionListQuery : IRequest<List<StoreAssetSubTypeOption>>
{
    public int? StoreAssetTypeId { get; set; }
    public bool UseManyToMany { get; set; } = false;
}

public class StoreAssetSubTypeOptionListHandler : ListQueryHandler, IRequestHandler<StoreAssetSubTypeOptionListQuery, List<StoreAssetSubTypeOption>>
{
    public StoreAssetSubTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<StoreAssetSubTypeOption>> Handle(StoreAssetSubTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreAssetSubTypes.AsQueryable().AsNoTracking();

        if (query.StoreAssetTypeId.HasValue)
        {
            if (!query.UseManyToMany)
            {
                queryable = queryable.Where(e => e.StoreAssetTypeId == query.StoreAssetTypeId);
            }
            else
            {
                var subTypeIds = await _context.StoreAssetTypeStoreAssetSubTypes.AsNoTracking()
                                               .Where(e => e.StoreAssetTypeId == query.StoreAssetTypeId)
                                               .Select(e => e.StoreAssetSubTypeId)
                                               .ToListAsync();
                queryable = queryable.Where(e => subTypeIds.Contains(e.StoreAssetSubTypeId));
            }
        }

        return await queryable.OrderBy(e => e.StoreAssetSubTypeId)
                              .ProjectTo<StoreAssetSubTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}