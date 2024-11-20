namespace Engage.Application.Services.ProjectFiles.Queries;

public class ProjectFilePaginatedQuery : PaginatedQuery, IRequest<ListResult<ProjectFileDto>>
{
}

public record ProjectFilePaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectFilePaginatedQuery, ListResult<ProjectFileDto>>
{
    public async Task<ListResult<ProjectFileDto>> Handle(ProjectFilePaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = ProjectFilePaginationProps.Create();

        var queryable = Context.ProjectFiles.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.ProjectFileId);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<ProjectFileDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}


