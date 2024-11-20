namespace Engage.Application.Services.OrderTemplateProducts.Commands;

public class OrderTemplateProductRecommendedPriceUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public decimal RecommendedPrice { get; set; }
}

public class OrderTemplateProductRecommendedPriceUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderTemplateProductRecommendedPriceUpdateCommand, OperationStatus>
{
    public OrderTemplateProductRecommendedPriceUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderTemplateProductRecommendedPriceUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplateProducts.SingleAsync(e => e.OrderTemplateProductId == command.Id, cancellationToken);
        entity.RecommendedPrice = command.RecommendedPrice;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.OrderTemplateProductId;
        return operationStatus;
    }
}

public class OrderTemplateProductRecommendedPriceUpdateValidator : AbstractValidator<OrderTemplateProductRecommendedPriceUpdateCommand>
{
    public OrderTemplateProductRecommendedPriceUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.RecommendedPrice).GreaterThanOrEqualTo(0);
    }
}