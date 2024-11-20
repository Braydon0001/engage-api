namespace Engage.Application.Services.ProjectPriorities.Queries;

public class ProjectPriorityListQuery : IRequest<List<ProjectPriorityDto>>
{

}

public record ProjectPriorityListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectPriorityListQuery, List<ProjectPriorityDto>>
{
    public async Task<List<ProjectPriorityDto>> Handle(ProjectPriorityListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectPriorities.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectPriorityId)
                              .ProjectTo<ProjectPriorityDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}