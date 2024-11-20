
namespace Engage.Application.Services.StoreAssetTypes.Queries;

public class StoreAssetTypeVmQuery : IRequest<StoreAssetTypeVm>
{
    public int Id { get; set; }
}
public record StoreAssetTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetTypeVmQuery, StoreAssetTypeVm>
{
    public async Task<StoreAssetTypeVm> Handle(StoreAssetTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetTypes.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.AssetOwnerAssetTypes)
                                 .ThenInclude(e => e.StoreAssetOwner)
                             .Include(e => e.AssetContacts)
                                 .ThenInclude(e => e.StoreAssetTypeContact)
                             .Include(e => e.AssetSubTypes)
                                 .ThenInclude(e => e.StoreAssetSubType);

        var entity = await queryable.SingleOrDefaultAsync(e => e.Id == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<StoreAssetTypeVm>(entity);

    }
}
