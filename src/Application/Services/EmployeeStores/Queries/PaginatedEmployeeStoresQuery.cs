using Engage.Application.Services.EmployeeStores.Models;
using Finbuckle.MultiTenant.Abstractions;

namespace Engage.Application.Services.EmployeeStores.Queries;

public class PaginatedEmployeeStoresQuery : GetQuery, IRequest<PaginatedListResult<EmployeeStoreDto>>
{
    public int? EmployeeId { get; set; }
    public int? StoreId { get; set; }
}

public class EmployeeStoresPaginatedQueryHandler : BaseQueryHandler, IRequestHandler<PaginatedEmployeeStoresQuery, PaginatedListResult<EmployeeStoreDto>>
{
    private readonly IMultiTenantContextAccessor _multiTenantContextAccessor;
    public EmployeeStoresPaginatedQueryHandler(IAppDbContext context, IMapper mapper, IMultiTenantContextAccessor multiTenantContextAccessor) : base(context, mapper)
    {
        _multiTenantContextAccessor = multiTenantContextAccessor;
    }

    public async Task<PaginatedListResult<EmployeeStoreDto>> Handle(PaginatedEmployeeStoresQuery request, CancellationToken cancellationToken)
    {
        var (query, pagination) = _context.EmployeeStores.Paginate(request, _multiTenantContextAccessor);

        var entities = await query.ProjectTo<EmployeeStoreDto>(_mapper.ConfigurationProvider)
                                  .ToListAsync(cancellationToken);

        return new PaginatedListResult<EmployeeStoreDto>(entities, pagination);
    }
}
