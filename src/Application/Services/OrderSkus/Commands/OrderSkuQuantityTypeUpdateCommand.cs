namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuQuantityTypeUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int OrderQuantityTypeId { get; set; }
}

public class OrderSkuQuantityTypeUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderSkuQuantityTypeUpdateCommand, OperationStatus>
{
    public OrderSkuQuantityTypeUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderSkuQuantityTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderSkus.SingleAsync(e => e.OrderSkuId == command.Id, cancellationToken);
        entity.OrderQuantityTypeId = command.OrderQuantityTypeId;

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.OrderSkuId;
        return opStatus;
    }
}
