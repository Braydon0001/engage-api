namespace Engage.Application.Services.WebFiles.Queries;

public class WebFilePaginatedQuery : PaginatedQuery, IRequest<ListResult<WebFileDto>>
{
}

public record WebFilePaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<WebFilePaginatedQuery, ListResult<WebFileDto>>
{
    public async Task<ListResult<WebFileDto>> Handle(WebFilePaginatedQuery query, CancellationToken cancellationToken)
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
                                      .ProjectTo<WebFileDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}


