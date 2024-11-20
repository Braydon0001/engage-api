namespace Engage.Application.Services.OrderSkus.Commands;

public class CreateOrderSkusWithDateCommand : IRequest<OperationStatus>
{
    public int OrderId { get; set; }
    public List<int> DcProductIds { get; set; }
    public DateTime? DeliveryDate { get; set; }
}

public class CreateOrderSkusWithDateCommandHandler : IRequestHandler<CreateOrderSkusWithDateCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly OrderDefaultsOptions _orderDefaults;

    public CreateOrderSkusWithDateCommandHandler(IAppDbContext context, IMediator mediator, IOptions<OrderDefaultsOptions> options)
    {
        _context = context;
        _mediator = mediator;
        _orderDefaults = options.Value;
    }

    public async Task<OperationStatus> Handle(CreateOrderSkusWithDateCommand command, CancellationToken cancellationToken)
    {
        foreach (var id in command.DcProductIds)
        {
            await _mediator.Send(new CreateOrderSkuWithDateCommand()
            {
                OrderId = command.OrderId,
                OrderSkuStatusId = 1,
                OrderSkuTypeId = _orderDefaults.SkuTypeId,
                OrderQuantityTypeId = _orderDefaults.SkuQuantityTypeId,
                DCProductId = id,
                Quantity = 0,
                DeliveryDate = command.DeliveryDate,
                SaveChanges = false
            }, cancellationToken);
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = command.OrderId;
        return opStatus;
    }
}
