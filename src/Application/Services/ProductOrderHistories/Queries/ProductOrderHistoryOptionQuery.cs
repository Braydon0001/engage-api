namespace Engage.Application.Services.ProductOrderHistories.Queries;

public class ProductOrderHistoryOptionQuery : IRequest<List<ProductOrderHistoryOption>>
{ 

}

public record ProductOrderHistoryOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderHistoryOptionQuery, List<ProductOrderHistoryOption>>
{
    public async Task<List<ProductOrderHistoryOption>> Handle(ProductOrderHistoryOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderHistories.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductOrderHistoryId)
                              .ProjectTo<ProductOrderHistoryOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}