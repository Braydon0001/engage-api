namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuPromotionPriceUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public decimal PromotionPrice { get; set; }
}

public class OrderSkuPromotionPriceUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderSkuPromotionPriceUpdateCommand, OperationStatus>
{
    public OrderSkuPromotionPriceUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderSkuPromotionPriceUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderSkus.SingleAsync(e => e.OrderSkuId == command.Id, cancellationToken);
        entity.PromotionPrice = command.PromotionPrice;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.OrderSkuId;
        return operationStatus;
    }
}

public class OrderSkuPromotionPriceUpdateValidator : AbstractValidator<OrderSkuPromotionPriceUpdateCommand>
{
    public OrderSkuPromotionPriceUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.PromotionPrice).GreaterThanOrEqualTo(0);
    }
}