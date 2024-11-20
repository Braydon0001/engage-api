namespace Engage.Application.Services.ProjectTypes.Queries;

public class ProjectTypeListQuery : IRequest<List<ProjectTypeDto>>
{

}

public record ProjectTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTypeListQuery, List<ProjectTypeDto>>
{
    public async Task<List<ProjectTypeDto>> Handle(ProjectTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProjectTypeId)
                              .ProjectTo<ProjectTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}