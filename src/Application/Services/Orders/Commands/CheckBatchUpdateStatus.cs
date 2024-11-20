using Engage.Application.Services.Orders.Models;

namespace Engage.Application.Services.Orders.Commands;

public class CheckBatchUpdateStatusCommand : IRequest<OperationStatus>
{
    public List<int> Ids { get; set; }
    public int OrderStatusId { get; set; }
}

public class CheckBatchUpdateStatusCommandHandler : BaseUpdateCommandHandler, IRequestHandler<CheckBatchUpdateStatusCommand, OperationStatus>
{
    public CheckBatchUpdateStatusCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(CheckBatchUpdateStatusCommand command, CancellationToken cancellationToken)
    {
        var processedOrders = new List<ProcessedOrderDto>();
        var orderIds = new List<int>();

        if (command.Ids.NotNullOrEmpty())
        {
            foreach (var id in command.Ids)
            {
                var order = await _context.Orders.SingleAsync(e => e.OrderId == id, cancellationToken);

                // Order already processed
                if (command.OrderStatusId == 3 && order.OrderStatusId == 3)
                {
                    processedOrders.Add(_mapper.Map<ProcessedOrderDto>(order));
                }
                else
                {
                    orderIds.Add(id);
                }
            }

            if (orderIds.Count > 0)
            {
                await _mediator.Send(new BatchUpdateOrderStatusCommand()
                {
                    Ids = orderIds,
                    OrderStatusId = command.OrderStatusId
                });

            }
        }

        var operationStatus = new OperationStatus
        {
            Status = true
        };

        if (processedOrders.Count > 0)
        {
            operationStatus.ReturnObject = processedOrders;
        }

        return operationStatus;
    }
}
