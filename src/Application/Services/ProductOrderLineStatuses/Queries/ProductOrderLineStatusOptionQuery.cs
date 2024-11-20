namespace Engage.Application.Services.ProductOrderLineStatuses.Queries;

public class ProductOrderLineStatusOptionQuery : IRequest<List<ProductOrderLineStatusOption>>
{ 

}

public record ProductOrderLineStatusOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineStatusOptionQuery, List<ProductOrderLineStatusOption>>
{
    public async Task<List<ProductOrderLineStatusOption>> Handle(ProductOrderLineStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderLineStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductOrderLineStatusId)
                              .ProjectTo<ProductOrderLineStatusOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}