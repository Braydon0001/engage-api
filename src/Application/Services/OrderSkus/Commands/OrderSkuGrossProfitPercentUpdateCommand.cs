namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuGrossProfitPercentUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public decimal GrossProfitPercent { get; set; }
}

public class OrderSkuGrossProfitPercentUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderSkuGrossProfitPercentUpdateCommand, OperationStatus>
{
    public OrderSkuGrossProfitPercentUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderSkuGrossProfitPercentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderSkus.SingleAsync(e => e.OrderSkuId == command.Id, cancellationToken);
        entity.GrossProfitPercent = command.GrossProfitPercent;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.OrderSkuId;
        return operationStatus;
    }
}

public class OrderSkuGrossProfitPercentUpdateValidator : AbstractValidator<OrderSkuGrossProfitPercentUpdateCommand>
{
    public OrderSkuGrossProfitPercentUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.GrossProfitPercent).GreaterThanOrEqualTo(0);
    }
}