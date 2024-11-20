namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuRecommendedPriceUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public decimal RecommendedPrice { get; set; }
}

public class OrderSkuRecommendedPriceUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderSkuRecommendedPriceUpdateCommand, OperationStatus>
{
    public OrderSkuRecommendedPriceUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderSkuRecommendedPriceUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderSkus.SingleAsync(e => e.OrderSkuId == command.Id, cancellationToken);
        entity.RecommendedPrice = command.RecommendedPrice;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.OrderSkuId;
        return operationStatus;
    }
}

public class OrderSkuRecommendedPriceUpdateValidator : AbstractValidator<OrderSkuRecommendedPriceUpdateCommand>
{
    public OrderSkuRecommendedPriceUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.RecommendedPrice).GreaterThanOrEqualTo(0);
    }
}