namespace Engage.Application.Services.Products.Queries;

public class ProductWithPriceQuery : IRequest<ProductWithPriceDto>
{
    public int Id { get; set; }
}
public record ProductWithPriceHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductWithPriceQuery, ProductWithPriceDto>
{
    public async Task<ProductWithPriceDto> Handle(ProductWithPriceQuery query, CancellationToken cancellationToken)
    {
        var product = await Context.Products
                                   .AsNoTracking()
                                   .Where(e => e.ProductId == query.Id)
                                   .ProjectTo<ProductWithPriceDto>(Mapper.ConfigurationProvider)
                                   .FirstOrDefaultAsync(cancellationToken);
        var productPrices = await Context.ProductPrices
                                         .AsNoTracking()
                                         .Where(e => e.ProductId == query.Id)
                                         .OrderBy(e => e.StartDate)
                                         .ToListAsync(cancellationToken);

        product.Price = productPrices.Count > 0 ? productPrices.Last().Price : 0;

        return product;
    }
}
