using Engage.Application.Services.EmployeeStores.Models;

namespace Engage.Application.Services.EmployeeStores.Queries;

public class EmployeeStorePaginatedQuery : PaginatedQuery, IRequest<ListResult<EmployeeStoreDto>>
{
}

public class EmployeeStorePaginatedHandler : ListQueryHandler, IRequestHandler<EmployeeStorePaginatedQuery, ListResult<EmployeeStoreDto>>
{
    public EmployeeStorePaginatedHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeStoreDto>> Handle(EmployeeStorePaginatedQuery query, CancellationToken cancellationToken)
    {
        var paginationModels = CreatePaginationProps();

        var queryable = _context.EmployeeStores.AsQueryable().AsNoTracking();

        var entities = await queryable.Filter(query, paginationModels)
                                      .Sort(query, paginationModels)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<EmployeeStoreDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<EmployeeStoreDto>(entities);
    }

    private static Dictionary<string, PaginationProperty> CreatePaginationProps()
    {
        return new Dictionary<string, PaginationProperty> {

            { "id", new PaginationProperty("EmployeeStoreId") },
            { "employeeName", new PaginationProperty("EmployeeId") },
            { "storeName", new PaginationProperty("StoreId") },
            { "engageSubGroupId", new PaginationProperty("EngageSubGroupId") },
            { "frequencyTypeId", new PaginationProperty("FrequencyTypeId") },
            { "frequency", new PaginationProperty("Frequency") },
        };
    }
}
