namespace Engage.Application.Services.Orders.Commands;

public class BatchUpdateOrderStatusCommand : IRequest<OperationStatus>
{
    public List<int> Ids { get; set; }
    public int OrderStatusId { get; set; }
}

public class BatchUpdateOrderStatusCommandHandler : BaseUpdateCommandHandler, IRequestHandler<BatchUpdateOrderStatusCommand, OperationStatus>
{
    public BatchUpdateOrderStatusCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }
    public async Task<OperationStatus> Handle(BatchUpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        if (command.Ids != null && command.Ids.Count > 0)
        {
            foreach (var id in command.Ids)
            {
                await _mediator.Send(new UpdateOrderStatusCommand()
                {
                    Id = id,
                    OrderStatusId = command.OrderStatusId,
                    SaveChanges = false
                }, cancellationToken);
            }
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            return opStatus;
        }

        return new OperationStatus(true);

    }
}
