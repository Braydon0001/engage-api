namespace Engage.Application.Services.ProjectStoreAssets.Queries;

public class ProjectStoreAssetOptionQuery : IRequest<List<ProjectStoreAssetOption>>
{ 

}

public record ProjectStoreAssetOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStoreAssetOptionQuery, List<ProjectStoreAssetOption>>
{
    public async Task<List<ProjectStoreAssetOption>> Handle(ProjectStoreAssetOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectStoreAssets.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectStoreAssetId)
                              .ProjectTo<ProjectStoreAssetOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}