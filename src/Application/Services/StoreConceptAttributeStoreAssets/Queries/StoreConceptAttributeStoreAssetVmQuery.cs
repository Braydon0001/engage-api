using Engage.Application.Services.StoreConceptAttributeStoreAssets.Models;

namespace Engage.Application.Services.StoreConceptAttributeStoreAssets.Queries;

public class StoreConceptAttributeStoreAssetVmQuery : IRequest<StoreConceptAttributeStoreAssetVm>
{
    public int StoreConceptAttributeId { get; set; }
    public int StoreAssetId { get; set; }
}

public class StoreConceptAttributeStoreAssetVmQueryHandler : BaseQueryHandler, IRequestHandler<StoreConceptAttributeStoreAssetVmQuery, StoreConceptAttributeStoreAssetVm>
{
    public StoreConceptAttributeStoreAssetVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreConceptAttributeStoreAssetVm> Handle(StoreConceptAttributeStoreAssetVmQuery request, CancellationToken cancellationToken)
    {

        var entity = await _context.StoreConceptAttributeStoreAssets.Include(e => e.StoreAsset)
                                                                    .Include(e => e.StoreConceptAttribute)
                                                                    .SingleAsync(x => x.StoreAssetId == request.StoreAssetId && x.StoreConceptAttributeId == request.StoreConceptAttributeId, cancellationToken);

        return _mapper.Map<StoreConceptAttributeStoreAsset, StoreConceptAttributeStoreAssetVm>(entity);
    }
}