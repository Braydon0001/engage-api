namespace Engage.Application.Services.CommunicationHistoryStores.Queries;

public class CommunicationHistoryStoreListQuery : IRequest<List<CommunicationHistoryStoreDto>>
{
    public int? StoreId { get; set; }
}

public record CommunicationHistoryStoreListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationHistoryStoreListQuery, List<CommunicationHistoryStoreDto>>
{
    public async Task<List<CommunicationHistoryStoreDto>> Handle(CommunicationHistoryStoreListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationHistoryStores.AsQueryable().AsNoTracking();

        if (query.StoreId.HasValue)
        {
            queryable = queryable.Where(e => e.StoreId == query.StoreId.Value);
        }

        return await queryable.OrderBy(e => e.CommunicationHistoryId)
                              .ProjectTo<CommunicationHistoryStoreDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}