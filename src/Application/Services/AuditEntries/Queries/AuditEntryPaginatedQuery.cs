namespace Engage.Application.Services.AuditEntries.Queries;

public class AuditEntryPaginatedQuery : PaginatedQuery, IRequest<List<AuditEntryDto>>
{
}

public record AuditEntryPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<AuditEntryPaginatedQuery, List<AuditEntryDto>>
{
    public async Task<List<AuditEntryDto>> Handle(AuditEntryPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = AuditEntryPaginationProps.Create();

        var queryable = Context.AuditEntries.AsQueryable().AsNoTracking();

        #region Custom Sort 
        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.AuditEntryID);
        }
        #endregion

        var data = await queryable.Filter(query, props)
                              .Sort(query, props)
                              .Skip(query.StartRow)
                              .Take(query.PageSize)
                              .ProjectTo<AuditEntryDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        return data;
    }
}