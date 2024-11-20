namespace Engage.Application.Services.ProductPrices.Queries;

public class ProductPriceListQuery : IRequest<List<ProductPriceDto>>
{

}

public record ProductPriceListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductPriceListQuery, List<ProductPriceDto>>
{
    public async Task<List<ProductPriceDto>> Handle(ProductPriceListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductPrices.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductPriceId)
                              .ProjectTo<ProductPriceDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}