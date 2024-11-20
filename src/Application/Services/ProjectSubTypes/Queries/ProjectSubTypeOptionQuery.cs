namespace Engage.Application.Services.ProjectSubTypes.Queries;

public class ProjectSubTypeOptionQuery : IRequest<List<ProjectSubTypeOption>>
{
    public int? ProjectTypeId { get; set; }
}

public record ProjectSubTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectSubTypeOptionQuery, List<ProjectSubTypeOption>>
{
    public async Task<List<ProjectSubTypeOption>> Handle(ProjectSubTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProjectSubTypes.AsQueryable().AsNoTracking();

        if (query.ProjectTypeId.HasValue)
        {
            queryable = queryable.Where(e => e.ProjectTypeId == query.ProjectTypeId);
        }

        return await queryable.OrderBy(e => e.ProjectSubTypeId)
                              .ProjectTo<ProjectSubTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}