namespace Engage.Application.Services.StoreAssetStatuses.Queries;

public record StoreAssetStatusVmQuery(int Id) : IRequest<StoreAssetStatusVm>;

public record StoreAssetStatusVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetStatusVmQuery, StoreAssetStatusVm>
{
    public async Task<StoreAssetStatusVm> Handle(StoreAssetStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.StoreAssetStatusId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<StoreAssetStatusVm>(entity);
    }
}