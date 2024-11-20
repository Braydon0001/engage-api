namespace Engage.Application.Services.OrderTemplateProducts.Commands;

public class OrderTemplateProductPromotionPriceUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public decimal PromotionPrice { get; set; }
}

public class OrderTemplateProductPromotionPriceUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderTemplateProductPromotionPriceUpdateCommand, OperationStatus>
{
    public OrderTemplateProductPromotionPriceUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderTemplateProductPromotionPriceUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplateProducts.SingleAsync(e => e.OrderTemplateProductId == command.Id, cancellationToken);
        entity.PromotionPrice = command.PromotionPrice;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.OrderTemplateProductId;
        return operationStatus;
    }
}

public class OrderTemplateProductPromotionPriceUpdateValidator : AbstractValidator<OrderTemplateProductPromotionPriceUpdateCommand>
{
    public OrderTemplateProductPromotionPriceUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.PromotionPrice).GreaterThanOrEqualTo(0);
    }
}