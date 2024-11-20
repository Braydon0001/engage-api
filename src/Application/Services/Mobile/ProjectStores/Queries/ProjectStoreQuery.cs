using Engage.Application.Services.ProjectFiles.Queries;
using Engage.Application.Services.ProjectProjectTags.Queries;

namespace Engage.Application.Services.Mobile.ProjectStores.Queries;

public record ProjectStoreQuery(int StoreId) : IRequest<ListResult<ProjectMobileDto>>;

public record ProjectStoreHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProjectStoreQuery, ListResult<ProjectMobileDto>>
{
    public async Task<ListResult<ProjectMobileDto>> Handle(ProjectStoreQuery query, CancellationToken cancellationToken)
    {
        var entities = await Context.ProjectStores.Where(e => e.StoreId == query.StoreId).OrderByDescending(e => e.Created).Include(e => e.ProjectTasks).ProjectTo<ProjectMobileDto>(Mapper.ConfigurationProvider).ToListAsync(cancellationToken);



        if (entities == null)
        {
            return null;
        }

        foreach (var entity in entities)
        {
            var tags = await Mediator.Send(new ProjectProjectTagListQuery { ProjectId = entity.Id }, cancellationToken);
            var files = await Mediator.Send(new ProjectFileListQuery { ProjectId = entity.Id }, cancellationToken);
            entity.ProjectTags = string.Join(", ", tags.Select(s => s.Name + " - " + s.Type));
            entity.Files = files.SelectMany(e => e.Files).ToList();
        }

        var data = new ListResult<ProjectMobileDto>(entities);

        return data;
    }
}