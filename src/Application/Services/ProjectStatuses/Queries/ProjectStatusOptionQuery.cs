namespace Engage.Application.Services.ProjectStatuses.Queries;

public class ProjectStatusOptionQuery : IRequest<List<ProjectStatusOption>>
{

}

public record ProjectStatusOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStatusOptionQuery, List<ProjectStatusOption>>
{
    public async Task<List<ProjectStatusOption>> Handle(ProjectStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectStatuses.IgnoreQueryFilters().AsQueryable().AsNoTracking().Where(e => e.Deleted == false && e.Disabled == false);

        return await queryable.OrderBy(e => e.ProjectStatusId)
                              .ProjectTo<ProjectStatusOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}