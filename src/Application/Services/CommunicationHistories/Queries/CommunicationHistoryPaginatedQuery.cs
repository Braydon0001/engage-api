namespace Engage.Application.Services.CommunicationHistories.Queries;

public class CommunicationHistoryPaginatedQuery : PaginatedQuery, IRequest<List<CommunicationHistoryDto>>
{
}

public record CommunicationHistoryPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationHistoryPaginatedQuery, List<CommunicationHistoryDto>>
{
    public async Task<List<CommunicationHistoryDto>> Handle(CommunicationHistoryPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = CommunicationHistoryProps.Create();

        var queryable = Context.CommunicationHistories.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.CommunicationHistoryId);
        }

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .SkipQuery(query)
                              .TakeQuery(query)
                              .ProjectTo<CommunicationHistoryDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}