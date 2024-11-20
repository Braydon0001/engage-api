namespace Engage.Application.Services.StoreAssetFileTypes.Queries;

public record StoreAssetFileTypeVmQuery(int Id) : IRequest<StoreAssetFileTypeVm>;

public record StoreAssetFileTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetFileTypeVmQuery, StoreAssetFileTypeVm>
{
    public async Task<StoreAssetFileTypeVm> Handle(StoreAssetFileTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.StoreAssetFileTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.StoreAssetFileTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<StoreAssetFileTypeVm>(entity);
    }
}