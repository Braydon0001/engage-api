namespace Engage.Application.Services.WhatsAppHistories.Queries;

public class WhatsAppHistoryPaginatedQuery : PaginatedQuery, IRequest<List<WhatsAppHistoryDto>>
{
}

public record WhatsAppHistoryPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<WhatsAppHistoryPaginatedQuery, List<WhatsAppHistoryDto>>
{
    public async Task<List<WhatsAppHistoryDto>> Handle(WhatsAppHistoryPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = WhatsAppHistoryProps.Create();

        var queryable = Context.WhatsAppHistories.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderByDescending(e => e.WhatsAppHistoryId);
        }

        return await queryable.Filter(query, props)
                              .Sort(query, props)
                              .SkipQuery(query)
                              .TakeQuery(query)
                              .ProjectTo<WhatsAppHistoryDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}