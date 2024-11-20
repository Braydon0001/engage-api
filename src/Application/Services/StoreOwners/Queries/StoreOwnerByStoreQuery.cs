

namespace Engage.Application.Services.StoreOwners.Queries;

public class StoreOwnerByStoreQuery : IRequest<List<StoreOwnerDto>>
{
    public int StoreId { get; set; }
}

public class StoreOwnerByStoreHandler: ListQueryHandler, IRequestHandler<StoreOwnerByStoreQuery, List<StoreOwnerDto>>
{
    public StoreOwnerByStoreHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {

    }

    public async Task<List<StoreOwnerDto>> Handle(StoreOwnerByStoreQuery query, CancellationToken cancellationToken) 
    {
        var queryable = _context.StoreOwners.Where(e => e.StoreId == query.StoreId).AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.StartDate)
                              .ThenBy(e => e.EndDate)
                              .ThenBy(e => e.StoreOwnerId)
                              .ProjectTo<StoreOwnerDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}