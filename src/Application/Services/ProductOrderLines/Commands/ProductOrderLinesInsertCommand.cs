namespace Engage.Application.Services.ProductOrderLines.Commands;

public class ProductOrderLinesInsertCommand : IRequest<List<ProductOrderLine>>
{
    public int ProductOrderId { get; set; }
    public List<int> ProductIds { get; set; }
}
public record ProductOrderLinesInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLinesInsertCommand, List<ProductOrderLine>>
{
    public async Task<List<ProductOrderLine>> Handle(ProductOrderLinesInsertCommand command, CancellationToken cancellationToken)
    {
        var order = await Context.ProductOrders.FirstOrDefaultAsync(e => e.ProductOrderId == command.ProductOrderId, cancellationToken)
                        ?? throw new Exception("Order not found");

        if (order.ProductOrderStatusId != (int)ProductOrderStatusEnum.Unsubmitted)
        {
            throw new Exception("Cannot add products to submitted order");
        }

        var existingLineProductIds = await Context.ProductOrderLines
                                        .Where(e => e.ProductOrderId == command.ProductOrderId)
                                        .Select(e => e.ProductId)
                                        .ToListAsync(cancellationToken);

        List<ProductOrderLine> orderLines = new();

        foreach (var productId in command.ProductIds)
        {
            if (existingLineProductIds.Contains(productId))
            {
                continue;
            }

            var productPrice = await Context.ProductPrices
                .Where(e => e.ProductId == productId)
                .OrderBy(e => e.Price)
                .ToListAsync(cancellationToken);

            orderLines.Add(new ProductOrderLine
            {
                ProductOrderId = command.ProductOrderId,
                ProductId = productId,
                ProductOrderLineStatusId = 1,
                ProductOrderLineTypeId = 1,
                Amount = productPrice.Count > 0 ? productPrice.Last().Price : 0,
                Quantity = 0,
            });
        }
        Context.ProductOrderLines.AddRange(orderLines);

        await Context.SaveChangesAsync(cancellationToken);

        return orderLines;
    }
}
public class ProductOrderLinesInsertValidator : AbstractValidator<ProductOrderLinesInsertCommand>
{
    public ProductOrderLinesInsertValidator()
    {
        RuleFor(e => e.ProductOrderId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductIds).NotEmpty();
    }
}