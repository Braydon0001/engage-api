namespace Engage.Application.Services.Stores.Queries;

public class StorePaginatedOptionQuery : PaginatedQuery, IRequest<List<StoreOption>>
{
}

public class StorePaginatedOptionHandler : ListQueryHandler, IRequestHandler<StorePaginatedOptionQuery, List<StoreOption>>
{
    public StorePaginatedOptionHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<StoreOption>> Handle(StorePaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = StorePaginationProps.Create();

        var queryable = _context.Stores.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.Name);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<StoreOption>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return entities;
    }
}