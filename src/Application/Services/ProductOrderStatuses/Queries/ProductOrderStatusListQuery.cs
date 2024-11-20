namespace Engage.Application.Services.ProductOrderStatuses.Queries;

public class ProductOrderStatusListQuery : IRequest<List<ProductOrderStatusDto>>
{

}

public record ProductOrderStatusListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderStatusListQuery, List<ProductOrderStatusDto>>
{
    public async Task<List<ProductOrderStatusDto>> Handle(ProductOrderStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductOrderStatusId)
                              .ProjectTo<ProductOrderStatusDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}