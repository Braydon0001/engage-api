namespace Engage.Application.Services.OrderStagingSkus.Queries;

public class OrderStagingSkuListQuery : IRequest<List<OrderStagingSkuDto>>
{

}

public record OrderStagingSkuListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrderStagingSkuListQuery, List<OrderStagingSkuDto>>
{
    public async Task<List<OrderStagingSkuDto>> Handle(OrderStagingSkuListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.OrderStagingSkus.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.OrderStagingSkuId)
                              .ProjectTo<OrderStagingSkuDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}