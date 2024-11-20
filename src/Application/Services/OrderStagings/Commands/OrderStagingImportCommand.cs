using Engage.Application.Services.OrderStagings.Queries;

namespace Engage.Application.Services.OrderStagings.Commands;

public class OrderStagingImportCommand : IRequest<List<int>>
{
    public List<OrderStagingImport> Orders { get; set; }
}

public record OrderStagingImportHandler(IMediator Mediator, IAppDbContext Context, ICsvService CsvService) : IRequestHandler<OrderStagingImportCommand, List<int>>
{
    public async Task<List<int>> Handle(OrderStagingImportCommand command, CancellationToken cancellationToken)
    {
        var orders = command.Orders.GroupBy(e => e.OrderNumber)
                         .OrderBy(e => e.Key)
                         .ToDictionary(key => key.Key, group => group.ToList());

        List<int> orderIds = [];

        foreach (var order in orders)
        {
            var orderItem = order.Value.First();

            ValidOrder(orderItem);

            foreach (var orderSku in order.Value)
            {
                ValidSku(orderItem);
            }

            OrderStaging staging = new()
            {
                Region = orderItem.Region,
                StoreName = orderItem.Store,
                AccountNumber = orderItem.AccountNumber,
                OrderNumber = orderItem.OrderNumber,
                OrderContactName = orderItem.OrderContact,
                OrderContactEmail = orderItem.OrderEmail,
                VatNumber = orderItem.VatNumber,
                Date = orderItem.OrderDate,
                Reference = orderItem.Reference
            };

            Context.OrderStagings.Add(staging);

            await Context.SaveChangesAsync(cancellationToken);

            orderIds.Add(staging.OrderStagingId);

            List<OrderStagingSku> skus = [];

            foreach (var sku in order.Value)
            {
                skus.Add(new()
                {
                    OrderStagingId = staging.OrderStagingId,
                    ProductName = sku.Product,
                    Barcode = sku.Barcode,
                    UnitType = sku.CaseType,
                    Quantity = sku.Quantity
                });
            }

            Context.OrderStagingSkus.AddRange(skus);

            await Context.SaveChangesAsync(cancellationToken);
        }

        return orderIds;
    }

    private static void ValidOrder(OrderStagingImport order)
    {
        if (order.Region.Length > 120)
            //return false;
            throw new Exception($"Region is too long on order Number {order.OrderNumber}");
        if (order.Store.Length > 120)
            //return false;
            throw new Exception($"Store is too long on order Number {order.OrderNumber}");
        if (order.AccountNumber.Length > 120)
            //return false;
            throw new Exception($"Account Number is too long on order Number {order.OrderNumber}");
        if (order.OrderNumber.Length > 120)
            //return false;
            throw new Exception($"Order Number is too long on order Number {order.OrderNumber}");
        if (order.OrderContact.Length > 120)
            //return false;
            throw new Exception($"Order Contact field is too long on order Number {order.OrderNumber}");
        if (order.OrderEmail.Length > 120)
            //return false;
            throw new Exception($"Order Email field is too long on order Number {order.OrderNumber}");
        if (order.VatNumber.Length > 120)
            //return false;
            throw new Exception($"Vat Number field is too long on order Number {order.OrderNumber}");
        if (order.OrderDate.Length > 60)
            //return false;
            throw new Exception($"Order Date field is too long on order Number {order.OrderNumber}");
        if (order.Reference.Length > 120)
            //return false;
            throw new Exception($"Reference field is too long on order Number {order.OrderNumber}");
        return;
    }

    private static void ValidSku(OrderStagingImport sku)
    {
        if (sku.Product.Length > 120)
            throw new Exception($"Product Name is too long on order Number {sku.OrderNumber}");
        if (sku.Barcode.Length > 120)
            throw new Exception($"Barcode is too long on order Number {sku.OrderNumber}");
        if (sku.CaseType.Length > 120)
            throw new Exception($"Case Type is too long on order Number {sku.OrderNumber}");
        return;
    }
}
