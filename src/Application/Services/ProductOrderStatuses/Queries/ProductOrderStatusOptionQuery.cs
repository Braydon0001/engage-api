namespace Engage.Application.Services.ProductOrderStatuses.Queries;

public class ProductOrderStatusOptionQuery : IRequest<List<ProductOrderStatusOption>>
{
    public bool ProccessOrders { get; set; }
}

public record ProductOrderStatusOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderStatusOptionQuery, List<ProductOrderStatusOption>>
{
    public async Task<List<ProductOrderStatusOption>> Handle(ProductOrderStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderStatuses.AsQueryable().AsNoTracking();

        if (query.ProccessOrders)
        {
            queryable = queryable.Where(e => e.ProductOrderStatusId > 2);
        }

        return await queryable.OrderBy(e => e.ProductOrderStatusId)
                              .ProjectTo<ProductOrderStatusOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}