namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuQuantityUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int Quantity { get; set; }
}

public class OrderSkuQuantityUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderSkuQuantityUpdateCommand, OperationStatus>
{
    public OrderSkuQuantityUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderSkuQuantityUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderSkus.SingleAsync(e => e.OrderSkuId == command.Id, cancellationToken);
        entity.Quantity = command.Quantity;

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.OrderSkuId;
        return operationStatus;
    }
}

public class OrderSkuQuantityUpdateValidator : AbstractValidator<OrderSkuQuantityUpdateCommand>
{
    public OrderSkuQuantityUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
    }
}