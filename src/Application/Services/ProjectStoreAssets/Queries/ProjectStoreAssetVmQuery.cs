namespace Engage.Application.Services.ProjectStoreAssets.Queries;

public record ProjectStoreAssetVmQuery(int Id) : IRequest<ProjectStoreAssetVm>;

public record ProjectStoreAssetVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStoreAssetVmQuery, ProjectStoreAssetVm>
{
    public async Task<ProjectStoreAssetVm> Handle(ProjectStoreAssetVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectStoreAssets.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Project)
                             .Include(e => e.StoreAsset);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProjectStoreAssetId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProjectStoreAssetVm>(entity);
    }
}