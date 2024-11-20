namespace Engage.Application.Services.ProjectTypes.Queries;

public class ProjectTypeOptionQuery : IRequest<List<ProjectTypeOption>>
{ 

}

public record ProjectTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTypeOptionQuery, List<ProjectTypeOption>>
{
    public async Task<List<ProjectTypeOption>> Handle(ProjectTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectTypeId)
                              .ProjectTo<ProjectTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}