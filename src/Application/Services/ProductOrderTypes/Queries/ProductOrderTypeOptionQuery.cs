namespace Engage.Application.Services.ProductOrderTypes.Queries;

public class ProductOrderTypeOptionQuery : IRequest<List<ProductOrderTypeOption>>
{ 

}

public record ProductOrderTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderTypeOptionQuery, List<ProductOrderTypeOption>>
{
    public async Task<List<ProductOrderTypeOption>> Handle(ProductOrderTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductOrderTypeId)
                              .ProjectTo<ProductOrderTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}