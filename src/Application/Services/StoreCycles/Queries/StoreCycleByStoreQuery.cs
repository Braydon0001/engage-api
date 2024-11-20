namespace Engage.Application.Services.StoreCycles.Queries;

public class StoreCycleByStoreQuery : IRequest<List<StoreCycleDto>>
{
    public int StoreId { get; set; }
}

public class StoreCycleByStoreHandler : ListQueryHandler, IRequestHandler<StoreCycleByStoreQuery, List<StoreCycleDto>>
{
    public StoreCycleByStoreHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {

    }

    public async Task<List<StoreCycleDto>> Handle(StoreCycleByStoreQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreCycles.Where(e => e.StoreId == query.StoreId).AsQueryable().AsNoTracking();

        return await queryable.OrderByDescending(e => e.StoreCycleId)
                              .ProjectTo<StoreCycleDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}