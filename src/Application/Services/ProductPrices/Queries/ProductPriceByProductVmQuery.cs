namespace Engage.Application.Services.ProductPrices.Queries;

public class ProductPriceByProductVmQuery : IRequest<ProductPriceByProductVm>
{
    public int Id { get; set; }
}

public record ProductPriceByProductVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductPriceByProductVmQuery, ProductPriceByProductVm>
{
    public async Task<ProductPriceByProductVm> Handle(ProductPriceByProductVmQuery query, CancellationToken cancellationToken)
    {
        var prices = await Context.ProductPrices.AsNoTracking()
                                                .Where(e => e.ProductId == query.Id)
                                                .OrderBy(e => e.StartDate)
                                                .ToListAsync(cancellationToken);
        if (prices.Count == 0)
        {
            return new ProductPriceByProductVm()
            {
                ProductId = query.Id,
                IsAvailable = false,
                Price = 0
            };
        }
        else
        {
            return new ProductPriceByProductVm()
            {
                ProductId = query.Id,
                IsAvailable = true,
                Price = prices.Last().Price,
            };
        }
    }
}
