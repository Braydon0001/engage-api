namespace Engage.Application.Services.ProjectTacOps.Queries;

public class ProjectTacOpListQuery : IRequest<List<ProjectTacOpDto>>
{
}

public record ProjectTacOpListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTacOpListQuery, List<ProjectTacOpDto>>
{
    public async Task<List<ProjectTacOpDto>> Handle(ProjectTacOpListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectTacOps.AsQueryable()
                                             .AsNoTracking();

        return await queryable.OrderByDescending(e => e.ProjectTacOpId)
                              .ProjectTo<ProjectTacOpDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}