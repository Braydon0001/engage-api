using Engage.Application.Services.SupplierStores.Models;

namespace Engage.Application.Services.SupplierStores.Queries;

public class SupplierStoresQuery : GetQuery, IRequest<ListResult<SupplierStoreDto>>
{
    public int? SupplierId { get; set; }
    public int? StoreId { get; set; }
}

public class SupplierStoresQueryHandler : BaseQueryHandler, IRequestHandler<SupplierStoresQuery, ListResult<SupplierStoreDto>>
{
    public SupplierStoresQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<SupplierStoreDto>> Handle(SupplierStoresQuery request, CancellationToken cancellationToken)
    {
        var supplierStores = await _context.SupplierStores.Where(x => x.SupplierId == (request.SupplierId ?? x.SupplierId) &&
                                                                      x.StoreId == (request.StoreId ?? x.StoreId))
                                                          .OrderBy(x => x.Supplier.Name)
                                                          .ThenBy(x => x.Store.Name)
                                                          .ThenBy(x => x.EngageSubGroup.Name)
                                                          .ProjectTo<SupplierStoreDto>(_mapper.ConfigurationProvider)
                                                          .ToListAsync(cancellationToken);

        return new ListResult<SupplierStoreDto>(supplierStores);
    }
}

