namespace Engage.Application.Services.StoreAssetFiles.Queries;

public record StoreAssetFileVmQuery(int Id) : IRequest<StoreAssetFileVm>;

public record StoreAssetFileVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetFileVmQuery, StoreAssetFileVm>
{
    public async Task<StoreAssetFileVm> Handle(StoreAssetFileVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetFiles.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.StoreAsset)
                             .Include(e => e.StoreAssetFileType);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.StoreAssetFileId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<StoreAssetFileVm>(entity);
    }
}