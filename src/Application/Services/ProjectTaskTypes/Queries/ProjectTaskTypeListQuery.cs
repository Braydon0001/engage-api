namespace Engage.Application.Services.ProjectTaskTypes.Queries;

public class ProjectTaskTypeListQuery : IRequest<List<ProjectTaskTypeDto>>
{

}

public record ProjectTaskTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskTypeListQuery, List<ProjectTaskTypeDto>>
{
    public async Task<List<ProjectTaskTypeDto>> Handle(ProjectTaskTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTaskTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectTaskTypeId)
                              .ProjectTo<ProjectTaskTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}