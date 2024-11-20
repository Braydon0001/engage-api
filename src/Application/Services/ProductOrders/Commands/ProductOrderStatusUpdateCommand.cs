using Engage.Application.Services.ProductTransactions.Commands;

namespace Engage.Application.Services.ProductOrders.Commands;

public class ProductOrderStatusUpdateCommand : IRequest<ProductOrder>
{
    public int Id { get; set; }
    public int ProductOrderStatusId { get; set; }
    public string Reason { get; set; }
}
public record ProductOrderStatusUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<ProductOrderStatusUpdateCommand, ProductOrder>
{
    public async Task<ProductOrder> Handle(ProductOrderStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var order = await Context.ProductOrders.FirstOrDefaultAsync(e => e.ProductOrderId == command.Id, cancellationToken)
            ?? throw new Exception("Order not found");

        ProductOrderHistory history = new()
        {
            ProductOrderId = order.ProductOrderId,
            ProductOrderStatusId = command.ProductOrderStatusId,
            Reason = command.Reason,
        };

        Context.ProductOrderHistories.Add(history);

        order.ProductOrderStatusId = command.ProductOrderStatusId;

        List<ProductTransaction> transactions = [];

        if (command.ProductOrderStatusId == (int)ProductOrderStatusEnum.Submitted)
        {
            var orderLines = await Context.ProductOrderLines.Where(e => e.ProductOrderId == command.Id).ToListAsync(cancellationToken);

            if (order.ProductOrderTypeId == (int)ProductOrderTypeEnum.Receipt)
            {
                foreach (var orderItem in orderLines)
                {
                    var transaction = await Mediator.Send(new ProductTransactionInsertCommand
                    {
                        ProductId = orderItem.ProductId,
                        ProductTransactionTypeId = (int)ProductTransactionTypeEnum.Receipt,
                        ProductWarehouseId = order.ProductWarehouseId,
                        Quantity = orderItem.Quantity,
                        Price = orderItem.Amount,
                        TransactionDate = DateTime.Now,
                        Note = orderItem.Note.IsNullOrEmpty() ? "" : orderItem.Note,
                        SaveChanges = false
                    }, cancellationToken);
                    transactions.AddRange(transaction);
                }

            }
            else if (order.ProductOrderTypeId == (int)ProductOrderTypeEnum.Transfer)
            {
                foreach (var orderItem in orderLines)
                {
                    var transaction = await Mediator.Send(new ProductTransactionInsertCommand
                    {
                        ProductId = orderItem.ProductId,
                        ProductTransactionTypeId = (int)ProductTransactionTypeEnum.TransferIn,
                        ProductWarehouseId = order.ProductWarehouseOutId.Value,
                        ProductWarehouseInId = order.ProductWarehouseId,
                        Quantity = orderItem.Quantity,
                        Price = orderItem.Amount,
                        TransactionDate = DateTime.Now,
                        Note = orderItem.Note.IsNullOrEmpty() ? "" : orderItem.Note,
                        SaveChanges = false
                    }, cancellationToken);
                    transactions.AddRange(transaction);
                }
            }
        }
        else if (command.ProductOrderStatusId == (int)ProductOrderStatusEnum.Rejected)
        {
            var orderLines = await Context.ProductOrderLines.Where(e => e.ProductOrderId == command.Id).ToListAsync(cancellationToken);

            if (order.ProductOrderTypeId == (int)ProductOrderTypeEnum.Receipt)
            {
                foreach (var orderItem in orderLines)
                {
                    await Mediator.Send(new ProductTransactionInsertCommand
                    {
                        ProductId = orderItem.ProductId,
                        ProductTransactionTypeId = (int)ProductTransactionTypeEnum.Adjustment,
                        ProductWarehouseId = order.ProductWarehouseId,
                        Quantity = orderItem.Quantity * -1,
                        Price = orderItem.Amount,
                        TransactionDate = DateTime.Now,
                        Note = orderItem.Note.IsNotNullOrEmpty() ? "" : orderItem.Note,
                        SaveChanges = false
                    }, cancellationToken);
                }
            }
            else if (order.ProductOrderTypeId == (int)ProductOrderTypeEnum.Transfer)
            {
                foreach (var orderItem in orderLines)
                {
                    await Mediator.Send(new ProductTransactionInsertCommand
                    {
                        ProductId = orderItem.ProductId,
                        ProductTransactionTypeId = (int)ProductTransactionTypeEnum.TransferIn,
                        ProductWarehouseId = order.ProductWarehouseId,
                        ProductWarehouseInId = order.ProductWarehouseOutId,
                        Quantity = orderItem.Quantity,
                        Price = orderItem.Amount,
                        TransactionDate = DateTime.Now,
                        Note = orderItem.Note.IsNullOrEmpty() ? "" : orderItem.Note,
                        SaveChanges = false
                    });
                }
            }
        }

        await Context.SaveChangesAsync(cancellationToken);

        return order;
    }
}
public class ProductOrderStatusUpdateValidator : AbstractValidator<ProductOrderStatusUpdateCommand>
{
    public ProductOrderStatusUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProductOrderStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Reason).MaximumLength(120);
    }
}