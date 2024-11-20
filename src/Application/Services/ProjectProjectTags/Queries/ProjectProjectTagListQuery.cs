namespace Engage.Application.Services.ProjectProjectTags.Queries;

public class ProjectProjectTagListQuery : IRequest<List<ProjectProjectTagDto>>
{
    public int ProjectId { get; set; }
}

public record ProjectProjectTagListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectProjectTagListQuery, List<ProjectProjectTagDto>>
{
    public async Task<List<ProjectProjectTagDto>> Handle(ProjectProjectTagListQuery query, CancellationToken cancellationToken)
    {
        if (query.ProjectId < 1)
        {
            throw new Exception("Project not found");
        }

        var project = await Context.Projects.FindAsync(query.ProjectId);

        if (project == null)
        {
            throw new Exception("Project not found");
        }

        var claims = await Context.ProjectProjectTagClaims.Where(p => p.ProjectId == query.ProjectId)
                                                          .ProjectTo<ProjectProjectTagDto>(Mapper.ConfigurationProvider)
                                                          .ToListAsync(cancellationToken);

        var dcProducts = await Context.ProjectProjectTagDCProducts.Where(p => p.ProjectId == query.ProjectId)
                                                                  .ProjectTo<ProjectProjectTagDto>(Mapper.ConfigurationProvider)
                                                                  .ToListAsync(cancellationToken);

        var jobTitles = await Context.ProjectProjectTagEmployeeJobTitles.Where(p => p.ProjectId == query.ProjectId)
                                                                        .ProjectTo<ProjectProjectTagDto>(Mapper.ConfigurationProvider)
                                                                        .ToListAsync(cancellationToken);

        var engageRegions = await Context.ProjectProjectTagEngageRegions.Where(p => p.ProjectId == query.ProjectId)
                                                                        .ProjectTo<ProjectProjectTagDto>(Mapper.ConfigurationProvider)
                                                                        .ToListAsync(cancellationToken);

        var stores = await Context.ProjectProjectTagStores.Where(p => p.ProjectId == query.ProjectId)
                                                          .ProjectTo<ProjectProjectTagDto>(Mapper.ConfigurationProvider)
                                                          .ToListAsync(cancellationToken);

        var storeAssets = await Context.ProjectProjectTagStoreAssets.Where(p => p.ProjectId == query.ProjectId)
                                                                    .ProjectTo<ProjectProjectTagDto>(Mapper.ConfigurationProvider)
                                                                    .ToListAsync(cancellationToken);

        var suppliers = await Context.ProjectProjectTagSuppliers.Where(p => p.ProjectId == query.ProjectId)
                                                                .ProjectTo<ProjectProjectTagDto>(Mapper.ConfigurationProvider)
                                                                .ToListAsync(cancellationToken);

        var users = await Context.ProjectProjectTagUsers.Where(p => p.ProjectId == query.ProjectId)
                                                        .ProjectTo<ProjectProjectTagDto>(Mapper.ConfigurationProvider)
                                                        .ToListAsync(cancellationToken);

        var tags = claims.Union(dcProducts)
                         .Union(jobTitles)
                         .Union(engageRegions)
                         .Union(stores)
                         .Union(storeAssets)
                         .Union(suppliers)
                         .Union(users)
                         .ToList();

        return tags;
    }
}