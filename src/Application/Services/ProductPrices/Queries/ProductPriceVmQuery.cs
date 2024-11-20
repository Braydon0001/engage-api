namespace Engage.Application.Services.ProductPrices.Queries;

public record ProductPriceVmQuery(int Id) : IRequest<ProductPriceVm>;

public record ProductPriceVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductPriceVmQuery, ProductPriceVm>
{
    public async Task<ProductPriceVm> Handle(ProductPriceVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductPrices.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Product);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductPriceId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProductPriceVm>(entity);
    }
}