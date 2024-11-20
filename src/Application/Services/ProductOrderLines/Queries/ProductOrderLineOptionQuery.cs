namespace Engage.Application.Services.ProductOrderLines.Queries;

public class ProductOrderLineOptionQuery : IRequest<List<ProductOrderLineOption>>
{ 

}

public record ProductOrderLineOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineOptionQuery, List<ProductOrderLineOption>>
{
    public async Task<List<ProductOrderLineOption>> Handle(ProductOrderLineOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderLines.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductOrderLineId)
                              .ProjectTo<ProductOrderLineOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}