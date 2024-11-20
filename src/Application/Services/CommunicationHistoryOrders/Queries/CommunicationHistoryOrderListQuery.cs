namespace Engage.Application.Services.CommunicationHistoryOrders.Queries;

public class CommunicationHistoryOrderListQuery : IRequest<List<CommunicationHistoryOrderDto>>
{
    public int? OrderId { get; set; }
}

public record CommunicationHistoryOrderListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationHistoryOrderListQuery, List<CommunicationHistoryOrderDto>>
{
    public async Task<List<CommunicationHistoryOrderDto>> Handle(CommunicationHistoryOrderListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationHistoryOrders.AsQueryable().AsNoTracking();

        if (query.OrderId.HasValue)
        {
            queryable = queryable.Where(e => e.OrderId == query.OrderId.Value);
        }

        return await queryable.OrderBy(e => e.CommunicationHistoryId)
                              .ProjectTo<CommunicationHistoryOrderDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}