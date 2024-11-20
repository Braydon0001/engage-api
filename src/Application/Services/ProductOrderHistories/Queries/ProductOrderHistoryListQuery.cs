namespace Engage.Application.Services.ProductOrderHistories.Queries;

public class ProductOrderHistoryListQuery : IRequest<List<ProductOrderHistoryDto>>
{

}

public record ProductOrderHistoryListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderHistoryListQuery, List<ProductOrderHistoryDto>>
{
    public async Task<List<ProductOrderHistoryDto>> Handle(ProductOrderHistoryListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderHistories.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductOrderHistoryId)
                              .ProjectTo<ProductOrderHistoryDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}