namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuPriceUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public decimal Price { get; set; }
}

public class OrderSkuPriceUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderSkuPriceUpdateCommand, OperationStatus>
{
    public OrderSkuPriceUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderSkuPriceUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderSkus.SingleAsync(e => e.OrderSkuId == command.Id, cancellationToken);
        entity.Price = command.Price;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.OrderSkuId;
        return operationStatus;
    }
}

public class OrderSkuPriceUpdateValidator : AbstractValidator<OrderSkuPriceUpdateCommand>
{
    public OrderSkuPriceUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
    }
}