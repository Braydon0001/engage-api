namespace Engage.Application.Services.ProductOrderLineStatuses.Queries;

public class ProductOrderLineStatusListQuery : IRequest<List<ProductOrderLineStatusDto>>
{

}

public record ProductOrderLineStatusListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineStatusListQuery, List<ProductOrderLineStatusDto>>
{
    public async Task<List<ProductOrderLineStatusDto>> Handle(ProductOrderLineStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderLineStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductOrderLineStatusId)
                              .ProjectTo<ProductOrderLineStatusDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}