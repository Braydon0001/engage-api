using Engage.Application.Services.StoreAssets.Models;

namespace Engage.Application.Services.StoreAssets.Queries;

public class StoreAssetVmQuery : IRequest<StoreAssetVm>
{
    public int Id { get; set; }
}

public class StoreAssetVMQueryHandler : BaseQueryHandler, IRequestHandler<StoreAssetVmQuery, StoreAssetVm>
{
    public StoreAssetVMQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreAssetVm> Handle(StoreAssetVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreAssets.Include(e => e.Store)
                                               .Include(e => e.StoreConceptAttributeStoreAssets)
                                               .ThenInclude(x => x.StoreConceptAttribute)
                                               .Include(e => e.AssetTypeContacts)
                                               .ThenInclude(x => x.StoreAssetTypeContact)
                                               .Include(e => e.StoreAssetType)
                                               .Include(e => e.StoreAssetSubType)
                                               .Include(e => e.StoreAssetOwner)
                                               //.Include(e => e.AssetStatus)
                                               .Include(e => e.StoreAssetCondition)
                                               .Include(e => e.StoreAssetStatus)
                                               .SingleAsync(x => x.StoreAssetId == request.Id, cancellationToken);

        var mappedEntity = _mapper.Map<StoreAsset, StoreAssetVm>(entity);

        var files = await _context.StoreAssetFiles.AsNoTracking()
                                            .Where(e => e.StoreAssetId == request.Id
                                                && e.StoreAssetFileTypeId == (int)StoreAssetFileTypeId.AssetContract)
                                            .ToListAsync(cancellationToken);

        mappedEntity.StoreAssetContract = new();

        foreach (var file in files)
        {
            mappedEntity.StoreAssetContract.AddRange(file.Files);
        }

        return mappedEntity;
    }
}
