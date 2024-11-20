namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuTemplateProductsInsertCommand : IRequest<OperationStatus>
{
    public int OrderId { get; set; }
    public int OrderTemplateId { get; set; }
}

public class OrderSkuTemplateProductsInsertHandler : BaseUpdateCommandHandler, IRequestHandler<OrderSkuTemplateProductsInsertCommand, OperationStatus>
{
    private readonly OrderDefaultsOptions _orderDefaults;

    public OrderSkuTemplateProductsInsertHandler(IAppDbContext context, IMapper mapper, IOptions<OrderDefaultsOptions> options) : base(context, mapper)
    {
        _orderDefaults = options.Value;
    }

    public async Task<OperationStatus> Handle(OrderSkuTemplateProductsInsertCommand command, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.SingleAsync(e => e.OrderId == command.OrderId, cancellationToken);

        var orderTemplateGroups = await _context.OrderTemplateGroups.Where(e => e.OrderTemplateId == command.OrderTemplateId).ToListAsync(cancellationToken);
        var orderTemplateGroupIds = orderTemplateGroups.Select(e => e.OrderTemplateGroupId).ToList();

        var orderTemplateProducts = await _context.OrderTemplateProducts.Where(e => orderTemplateGroupIds.Contains(e.OrderTemplateGroupId) && e.Disabled == false).ToListAsync(cancellationToken);

        foreach (var templateProduct in orderTemplateProducts)
        {
            var orderSku = new OrderSku
            {
                OrderId = command.OrderId,
                OrderSkuStatusId = 1,
                OrderSkuTypeId = _orderDefaults.SkuTypeId,
                OrderQuantityTypeId = _orderDefaults.SkuQuantityTypeId,
                OrderTemplateProductId = templateProduct.OrderTemplateProductId,
                DCProductId = templateProduct.DCProductId,
                Quantity = 0,
                Price = templateProduct.Price,
                PromotionPrice = templateProduct.PromotionPrice,
                RecommendedPrice = templateProduct.RecommendedPrice,
                GrossProfitPercent = templateProduct.GrossProfitPercent,
                Suffix = templateProduct.Suffix
            };

            order.OrderSkus.Add(orderSku);
        }

        order.OrderTemplateId = command.OrderTemplateId;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.OrderId;
        return operationStatus;
    }
}

public class OrderSkuTemplateProductsInsertValidator : AbstractValidator<OrderSkuTemplateProductsInsertCommand>
{
    public OrderSkuTemplateProductsInsertValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.OrderTemplateId).GreaterThan(0).NotEmpty();
    }
}