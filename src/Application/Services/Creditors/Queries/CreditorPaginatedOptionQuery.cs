namespace Engage.Application.Services.Creditors.Queries;

public class CreditorPaginatedOptionQuery : PaginatedQuery, IRequest<List<CreditorOption>>
{
}

public record CreditorPaginatedOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorPaginatedOptionQuery, List<CreditorOption>>
{
    public async Task<List<CreditorOption>> Handle(CreditorPaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = CreditorPaginationProps.Create();

        var queryable = Context.Creditors.Where(c => c.CreditorStatusId == (int)CreditorStatusId.Approved)
                                         .AsQueryable()
                                         .AsNoTracking();

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<CreditorOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


