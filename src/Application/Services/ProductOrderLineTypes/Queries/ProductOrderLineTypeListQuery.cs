namespace Engage.Application.Services.ProductOrderLineTypes.Queries;

public class ProductOrderLineTypeListQuery : IRequest<List<ProductOrderLineTypeDto>>
{

}

public record ProductOrderLineTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineTypeListQuery, List<ProductOrderLineTypeDto>>
{
    public async Task<List<ProductOrderLineTypeDto>> Handle(ProductOrderLineTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderLineTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductOrderLineTypeId)
                              .ProjectTo<ProductOrderLineTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}