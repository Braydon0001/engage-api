namespace Engage.Application.Services.ProjectStoreAssets.Queries;

public class ProjectStoreAssetListQuery : IRequest<List<ProjectStoreAssetDto>>
{

}

public record ProjectStoreAssetListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStoreAssetListQuery, List<ProjectStoreAssetDto>>
{
    public async Task<List<ProjectStoreAssetDto>> Handle(ProjectStoreAssetListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectStoreAssets.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectStoreAssetId)
                              .ProjectTo<ProjectStoreAssetDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}