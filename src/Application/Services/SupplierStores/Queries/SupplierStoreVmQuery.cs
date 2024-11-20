using Engage.Application.Services.SupplierStores.Models;

namespace Engage.Application.Services.SupplierStores.Queries;

public class SupplierStoreVmQuery : IRequest<SupplierStoreVm>
{
    public int Id { get; set; }
}


public class SupplierStoreVmQueryHandler : BaseQueryHandler, IRequestHandler<SupplierStoreVmQuery, SupplierStoreVm>
{
    public SupplierStoreVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierStoreVm> Handle(SupplierStoreVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierStores.Include(x => x.Supplier)
                                                  .Include(x => x.SupplierRegion)
                                                  .Include(x => x.SupplierSubRegion)
                                                  .Include(x => x.Store)
                                                  .Include(x => x.EngageSubGroup)
                                                  .Include(x => x.FrequencyType)
                                                  .SingleAsync(x => x.SupplierStoreId == request.Id, cancellationToken);

        return _mapper.Map<SupplierStore, SupplierStoreVm>(entity);
    }
}
