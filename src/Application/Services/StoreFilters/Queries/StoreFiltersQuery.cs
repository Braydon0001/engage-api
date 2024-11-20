using Engage.Application.Services.StoreFilters.Models;

namespace Engage.Application.Services.StoreFilters.Queries;

public class StoreFiltersQuery : IRequest<ListResult<StoreFilterDto>>
{
    public List<int> StoreIds { get; set; }
    public List<string> Filters { get; set; }
}

public class StoreFiltersQueryHandler : BaseQueryHandler, IRequestHandler<StoreFiltersQuery, ListResult<StoreFilterDto>>
{
    public StoreFiltersQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<StoreFilterDto>> Handle(StoreFiltersQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreFilters.AsQueryable();

        if (request.StoreIds != null && request.StoreIds.Count > 0)
        {
            queryable = queryable.Where(e => request.StoreIds.Contains(e.StoreId));
        }

        if (request.Filters != null && request.Filters.Count > 0)
        {
            queryable = queryable.Where(e => request.Filters.Contains(e.Filter));
        }

        var entities = await _context.StoreFilters.OrderBy(e => e.Filter)
                                                  .ThenBy(e => e.Store.Name)
                                                  .ProjectTo<StoreFilterDto>(_mapper.ConfigurationProvider)
                                                  .ToListAsync(cancellationToken);

        return new ListResult<StoreFilterDto>(entities);
    }
}
