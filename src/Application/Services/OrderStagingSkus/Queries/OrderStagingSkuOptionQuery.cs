namespace Engage.Application.Services.OrderStagingSkus.Queries;

public class OrderStagingSkuOptionQuery : IRequest<List<OrderStagingSkuOption>>
{ 

}

public record OrderStagingSkuOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrderStagingSkuOptionQuery, List<OrderStagingSkuOption>>
{
    public async Task<List<OrderStagingSkuOption>> Handle(OrderStagingSkuOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.OrderStagingSkus.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.OrderStagingSkuId)
                              .ProjectTo<OrderStagingSkuOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}