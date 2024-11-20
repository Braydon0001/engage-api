namespace Engage.Application.Services.ProductOrderTypes.Queries;

public class ProductOrderTypeListQuery : IRequest<List<ProductOrderTypeDto>>
{

}

public record ProductOrderTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderTypeListQuery, List<ProductOrderTypeDto>>
{
    public async Task<List<ProductOrderTypeDto>> Handle(ProductOrderTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductOrderTypeId)
                              .ProjectTo<ProductOrderTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}