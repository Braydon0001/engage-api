using Engage.Application.Services.ProductPeriods.Queries;

namespace Engage.Application.Services.ProductOrderLines.Commands;

public class ProductOrderLineQuantityUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public float Quantity { get; set; }
}
public record ProductOrderLineQuantityUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProductOrderLineQuantityUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProductOrderLineQuantityUpdateCommand command, CancellationToken cancellationToken)
    {
        var orderLine = await Context.ProductOrderLines
                    .Include(e => e.ProductOrder)
                    .FirstOrDefaultAsync(e => e.ProductOrderLineId == command.Id, cancellationToken);

        if (orderLine == null)
            return null;

        if (orderLine.ProductOrder.ProductOrderTypeId == (int)ProductOrderTypeEnum.Transfer)
        {
            var productPeriods = await Mediator.Send(new ProductPeriodCurrentPreviousIdQuery(), cancellationToken);

            var warehouseSummary = await Context.ProductWarehouseSummaries.AsNoTracking()
                                                .Where(e => e.ProductId == orderLine.ProductId
                                                    && e.ProductWarehouseId == orderLine.ProductOrder.ProductWarehouseOutId.Value
                                                    && e.ProductPeriodId == productPeriods.PreviousPeriodId)
                                                .FirstOrDefaultAsync(cancellationToken);

            var transactions = await Context.ProductTransactions
                                                .Where(e => e.ProductId == orderLine.ProductId
                                                    && e.ProductWarehouseId == orderLine.ProductOrder.ProductWarehouseOutId.Value
                                                    && e.ProductPeriodId == productPeriods.CurrentPeriodId)
                                                .ToListAsync(cancellationToken);
            var stockCount = warehouseSummary.Quantity + transactions.Select(e => e.Quantity).Sum();

            if (stockCount < command.Quantity)
            {
                //command.Quantity = stockCount;
                throw new Exception("Can't order more than available stock");
            }
        }

        orderLine.Quantity = command.Quantity;

        await Context.SaveChangesAsync(cancellationToken);

        return new OperationStatus
        {
            Status = true,
            OperationId = orderLine.ProductOrderLineId,
            RecordsAffected = 1,
        };
    }
}
public class ProductOrderLineQuantityUpdateValidator : AbstractValidator<ProductOrderLineQuantityUpdateCommand>
{
    public ProductOrderLineQuantityUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Quantity).NotEmpty().GreaterThan(0);
    }
}