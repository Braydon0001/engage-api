namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuUpdateCommand : OrderSkuCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class OrderSkuUpdateCommand2 : IRequest<OperationStatus>
{
    public int OrderSkuId { get; set; }
    public int DcProductId { get; set; }
    public int OrderQuantityTypeId { get; set; }
    public int Quantity { get; set; }
    public string Note { get; set; }
}

public class OrderSkuUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<OrderSkuUpdateCommand, OperationStatus>
{
    public OrderSkuUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(OrderSkuUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderSkus.SingleAsync(x => x.OrderSkuId == command.Id, cancellationToken);
        return await SaveChangesAsync(command, entity, nameof(OrderSkus), command.Id, cancellationToken);
    }
}
