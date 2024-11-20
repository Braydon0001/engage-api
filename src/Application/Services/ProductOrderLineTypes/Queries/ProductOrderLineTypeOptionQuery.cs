namespace Engage.Application.Services.ProductOrderLineTypes.Queries;

public class ProductOrderLineTypeOptionQuery : IRequest<List<ProductOrderLineTypeOption>>
{ 

}

public record ProductOrderLineTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineTypeOptionQuery, List<ProductOrderLineTypeOption>>
{
    public async Task<List<ProductOrderLineTypeOption>> Handle(ProductOrderLineTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderLineTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductOrderLineTypeId)
                              .ProjectTo<ProductOrderLineTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}