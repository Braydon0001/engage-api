namespace Engage.Application.Services.ProjectStatuses.Queries;

public class ProjectStatusListQuery : IRequest<List<ProjectStatusDto>>
{

}

public record ProjectStatusListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStatusListQuery, List<ProjectStatusDto>>
{
    public async Task<List<ProjectStatusDto>> Handle(ProjectStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectStatusId)
                              .ProjectTo<ProjectStatusDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}