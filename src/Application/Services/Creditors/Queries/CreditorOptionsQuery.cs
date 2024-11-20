namespace Engage.Application.Services.Creditors.Queries;

public class CreditorOptionsQuery : GetQuery, IRequest<List<CreditorOption>>
{
}

public record CreditorOptionsHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorOptionsQuery, List<CreditorOption>>
{
    public async Task<List<CreditorOption>> Handle(CreditorOptionsQuery query, CancellationToken cancellationToken)
    {
        var props = CreditorPaginationProps.Create();

        var queryable = Context.Creditors.Where(c => c.CreditorStatusId == (int)CreditorStatusId.Approved)
                                         .AsQueryable()
                                         .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{query.Search}%"));
        }

        return await queryable.Take(100)
                              .ProjectTo<CreditorOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}