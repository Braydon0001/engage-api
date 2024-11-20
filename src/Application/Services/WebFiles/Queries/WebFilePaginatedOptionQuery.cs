namespace Engage.Application.Services.WebFiles.Queries;

public class WebFilePaginatedOptionQuery : PaginatedQuery, IRequest<List<WebFileOption>>
{
}

public record WebFilePaginatedOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<WebFilePaginatedOptionQuery, List<WebFileOption>>
{
    public async Task<List<WebFileOption>> Handle(WebFilePaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = WebFilePaginationProps.Create();

        var queryable = Context.WebFiles.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.WebFileId);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<WebFileOption>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return entities;
    }
}


