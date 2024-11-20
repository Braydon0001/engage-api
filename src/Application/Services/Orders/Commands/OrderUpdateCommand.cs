using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.Orders.Commands;

public class UpdateOrderCommand : OrderCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateOrderCommand2 : IRequest<OperationStatus>
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string OrderReference { get; set; }

}

public class UpdateOrderCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateOrderCommand, OperationStatus>
{
    public UpdateOrderCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders.SingleAsync(x => x.OrderId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        if (command.EngageDepartmentIds != null)
        {
            await _mediator.Send(new BatchAssignCommand(
                AssignDesc.ENGAGE_DEPARTMENT_ORDER, command.Id, command.EngageDepartmentIds));
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.OrderId;
        return opStatus;
    }
}
