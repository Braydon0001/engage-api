namespace Engage.Application.Services.Mobile.ProjectStores.Queries;

public record ProjectStoreWithTaskQuery(int StoreId, int TaskId) : IRequest<ProjectWithTasks>;

public record ProjectStoreWithTaskHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectStoreWithTaskQuery, ProjectWithTasks>
{
    public async Task<ProjectWithTasks> Handle(ProjectStoreWithTaskQuery query, CancellationToken cancellationToken)
    {

        var queryable = Context.ProjectStores.AsQueryable().AsNoTracking();



        queryable = queryable.Where(e => e.StoreId == query.StoreId);

        var project = await Context.ProjectTasks.AsNoTracking().FirstOrDefaultAsync(e => e.ProjectTaskId == query.TaskId, cancellationToken);


        var projectId = project.ProjectId;


        var storeProject = await queryable
                                .ProjectTo<ProjectWithTasks>(Mapper.ConfigurationProvider)
                                .FirstOrDefaultAsync(e => e.Id == projectId);

        var dcProducts = await Context.ProjectProjectTagDCProducts.Where(p => p.ProjectId == storeProject.Id)
                                                           .ProjectTo<ProjectProjectTagMobileDto>(Mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

        var storeAssets = await Context.ProjectProjectTagStoreAssets.Where(p => p.ProjectId == storeProject.Id)
                                                           .ProjectTo<ProjectProjectTagMobileDto>(Mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

        var projectUsers = await Context.ProjectStakeholderUsers.Where(p => p.ProjectId == storeProject.Id)
                                                            .Include(e => e.User)
                                                           .ToListAsync(cancellationToken);

        storeProject.DcProductTagIds = dcProducts;
        storeProject.StoreAssetTagIds = storeAssets;
        storeProject.ProjectUsersTags = projectUsers.Select(e => new OptionDto { Id = e.UserId, Name = $"{e.User.FirstName} {e.User.LastName}" }).ToList();

        storeProject.ProjectTasks = storeProject.ProjectTasks.Where(e => e.Id == query.TaskId).ToList();

        return storeProject;




    }
}