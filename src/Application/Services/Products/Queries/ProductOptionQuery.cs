using Engage.Application.Services.ProductPeriods.Queries;

namespace Engage.Application.Services.Products.Queries;

public class ProductOptionQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int ProductSupplierId { get; set; }
    public int ProductWarehouseOutId { get; set; }
}
public class ProductOptionQueryHandler : ListQueryHandler, IRequestHandler<ProductOptionQuery, List<OptionDto>>
{
    private readonly IMediator _mediator;
    public ProductOptionQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<List<OptionDto>> Handle(ProductOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.Products.AsQueryable().AsNoTracking();

        if (query.ProductSupplierId > 0)
        {
            var manufactererIds = await _context.ProductManufacturers
                                                    .AsNoTracking()
                                                    .Where(e => e.ProductSupplierId == query.ProductSupplierId)
                                                    .Select(e => e.ProductManufacturerId)
                                                    .ToListAsync(cancellationToken);
            queryable = queryable.Where(e => manufactererIds.Contains(e.ProductMaster.ProductManufacturerId));
        }

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            queryable = queryable.Where(e => (EF.Functions.Like(e.Code, $"%{query.Search}%"))
                                        || (EF.Functions.Like(e.Name, $"%{query.Search}%"))
                                        );
        }

        var entities = await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.ProductId, Name = e.Code + " - " + e.Name })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);

        if (query.ProductWarehouseOutId > 0)
        {
            var periodIds = await _mediator.Send(new ProductPeriodCurrentPreviousIdQuery(), cancellationToken);
            var stockIds = await FilterByStockOnHand(entities.Select(e => e.Id).ToList(), query.ProductWarehouseOutId, periodIds, cancellationToken);

            return entities.Where(e => stockIds.Contains(e.Id)).ToList();
        }

        return entities;
    }

    private async Task<List<int>> FilterByStockOnHand(List<int> products, int productWarehouseOutId, ProductPeriodCurrentPreviousIdDto periodIds, CancellationToken cancellationToken)
    {
        var productWarehouseSummaries = await _context.ProductWarehouseSummaries
                                                 .AsNoTracking()
                                                 .Where(e => e.ProductPeriodId == periodIds.PreviousPeriodId
                                                    && e.ProductWarehouseId == productWarehouseOutId
                                                    && products.Contains(e.ProductId))
                                                 .ToListAsync(cancellationToken);

        var productWarehouseSummaryIds = productWarehouseSummaries.Select(e => e.ProductId).ToList();

        var transactions = await _context.ProductTransactions.AsNoTracking()
                                                       .Where(e => e.ProductWarehouseId == productWarehouseOutId
                                                            && products.Contains(e.ProductId)
                                                            && e.ProductPeriodId == periodIds.CurrentPeriodId)
                                                       .ToListAsync(cancellationToken);

        var transactionIds = transactions.Select(e => e.ProductId).Distinct().ToList();

        List<StagingObject> stockOnHand = productWarehouseSummaries.Select(e =>
        new StagingObject
        {
            ProductId = e.ProductId,
            Quantity = e.Quantity + transactions.Where(e => e.ProductId == e.ProductId).Select(e => e.Quantity).Sum(),
        })
        .ToList();
        foreach (var id in transactionIds.Where(e => !productWarehouseSummaryIds.Contains(e)).ToList())
        {
            stockOnHand.Add(new StagingObject
            {
                ProductId = id,
                Quantity = transactions.Where(e => e.ProductId == id).Select(e => e.Quantity).Sum(),
            });
        }

        return stockOnHand.Where(e => e.Quantity > 0).Select(e => e.ProductId).ToList();
    }

    private class StagingObject
    {
        public int ProductId { get; set; }
        public double Quantity { get; set; }
    }
}