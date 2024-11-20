namespace Engage.Application.Services.ProjectSubTypes.Queries;

public class ProjectSubTypeListQuery : IRequest<List<ProjectSubTypeDto>>
{

}

public record ProjectSubTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSubTypeListQuery, List<ProjectSubTypeDto>>
{
    public async Task<List<ProjectSubTypeDto>> Handle(ProjectSubTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectSubTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectSubTypeId)
                              .ProjectTo<ProjectSubTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}