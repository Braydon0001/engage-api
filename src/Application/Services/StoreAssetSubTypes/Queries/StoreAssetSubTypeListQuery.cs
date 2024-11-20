// auto-generated
namespace Engage.Application.Services.StoreAssetSubTypes.Queries;

public class StoreAssetSubTypeListQuery : IRequest<List<StoreAssetSubTypeDto>>
{

}

public class StoreAssetSubTypeListHandler : ListQueryHandler, IRequestHandler<StoreAssetSubTypeListQuery, List<StoreAssetSubTypeDto>>
{
    public StoreAssetSubTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<StoreAssetSubTypeDto>> Handle(StoreAssetSubTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreAssetSubTypes.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.StoreAssetTypes)
                             .ThenInclude(e => e.StoreAssetType);

        return await queryable.OrderBy(e => e.StoreAssetType.Name)
                              .ThenBy(e => e.Name)
                              .ProjectTo<StoreAssetSubTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}