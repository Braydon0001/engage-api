using Engage.Application.Services.EmployeeStores.Models;

namespace Engage.Application.Services.EmployeeStores.Queries;

public class EmployeeStoresQuery : GetQuery, IRequest<ListResult<EmployeeStoreDto>>
{
    public int? EmployeeId { get; set; }
    public int? StoreId { get; set; }
}

public class EmployeeStoresQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeStoresQuery, ListResult<EmployeeStoreDto>>
{
    public EmployeeStoresQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeStoreDto>> Handle(EmployeeStoresQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStores.IgnoreQueryFilters()
                                               .AsQueryable();

        if (request.EmployeeId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeId == request.EmployeeId.Value);
        }

        if (request.StoreId.HasValue)
        {
            queryable = queryable.Where(e => e.StoreId == request.StoreId.Value);
        }

        var entities = await queryable.OrderBy(x => x.Employee.FirstName)
                                      .ThenBy(x => x.Employee.LastName)
                                      .ThenBy(x => x.Store.Name)
                                      .ThenBy(x => x.EngageSubGroup.EngageDepartment.Name)
                                      .ThenBy(x => x.EngageSubGroup.Name)
                                      .ProjectTo<EmployeeStoreDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<EmployeeStoreDto>(entities);
    }
}
