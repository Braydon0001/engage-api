using Engage.Application.Services.Stores.Models;

namespace Engage.Application.Services.Stores.Queries;

public class StoreVmQuery : GetByIdQuery, IRequest<StoreVm>
{
}

public class StoreVmQueryHandler : BaseQueryHandler, IRequestHandler<StoreVmQuery, StoreVm>
{
    public StoreVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<StoreVm> Handle(StoreVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Stores.IgnoreQueryFilters()
                                          .Include(x => x.StoreCluster)
                                          .Include(x => x.StoreFormat)
                                          .Include(x => x.StoreGroup)
                                          .Include(x => x.StoreLSM)
                                          .Include(x => x.StoreMediaGroup)
                                          .Include(x => x.StoreSparRegion)
                                          .Include(x => x.StoreType)
                                          .Include(x => x.StoreLocationType)
                                          .Include(x => x.EngageRegion)
                                          .Include(x => x.EngageSubRegion)
                                          .Include(x => x.StoreStoreDepartments)
                                          .ThenInclude(x => x.StoreDepartment)
                                          .Include(x => x.StoreConceptLevels)
                                          .ThenInclude(x => x.StoreConcept)
                                          .SingleAsync(x => x.StoreId == request.Id, cancellationToken);

        return _mapper.Map<Store, StoreVm>(entity);
    }
}
