using Engage.Application.Services.ProductOrderLines.Queries;

namespace Engage.Application.Services.ProductOrders.Queries;

public record ProductOrderSummaryQuery(int Id) : IRequest<ProductOrderSummaryDto>;

public record ProductOrderSummaryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderSummaryQuery, ProductOrderSummaryDto>
{
    public async Task<ProductOrderSummaryDto> Handle(ProductOrderSummaryQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrders.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductOrderId == query.Id, cancellationToken);

        var order = Mapper.Map<ProductOrderSummaryDto>(entity);

        var lines = await Context.ProductOrderLines
                            .AsNoTracking()
                            .Where(e => e.ProductOrderId == query.Id)
                            .ProjectTo<ProductOrderLineDto>(Mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

        order.ProductOrderLines = lines;

        var total = lines.Select(e => (decimal)e.Quantity * e.Amount).ToList().Sum();

        order.PriceMissingCount = lines.Where(e => e.Amount == 0).ToList().Count;

        order.TotalAmount = total;

        return order;
    }
}
