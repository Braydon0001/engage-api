namespace Engage.Application.Services.Creditors.Queries;

public class CreditorPaginatedQuery : PaginatedQuery, IRequest<List<CreditorDto>>
{
}

public record CreditorPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorPaginatedQuery, List<CreditorDto>>
{
    public async Task<List<CreditorDto>> Handle(CreditorPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = CreditorPaginationProps.Create();

        var queryable = Context.Creditors.AsQueryable().AsNoTracking();

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<CreditorDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}


