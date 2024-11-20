namespace Engage.Application.Services.StoreAssetTypeContacts.Queries;

public record StoreAssetTypeContactVmQuery(int Id) : IRequest<StoreAssetTypeContactVm>;

public record StoreAssetTypeContactVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetTypeContactVmQuery, StoreAssetTypeContactVm>
{
    public async Task<StoreAssetTypeContactVm> Handle(StoreAssetTypeContactVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetTypeContacts.AsQueryable().AsNoTracking();

        var entity = await queryable.Include(e => e.StoreAssetTypes)
                                    .ThenInclude(e => e.StoreAssetType)
                                    .SingleOrDefaultAsync(e => e.StoreAssetTypeContactId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<StoreAssetTypeContactVm>(entity);
    }
}