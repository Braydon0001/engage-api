using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.Orders.Commands;

// Commands
public class UpdateOrderEngageDepartmentsCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public List<int> EngageDepartmentIds { get; set; }
}

public class UpdateOrderDateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
}

public class UpdateOrderDeliveryDateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public DateTime DeliveryDate { get; set; }
}

public class UpdateOrderReferenceCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string OrderReference { get; set; }
}

// Handlers
public class UpdateOrderEngageDepartmentsCommandHandler : IRequestHandler<UpdateOrderEngageDepartmentsCommand, OperationStatus>
{
    private readonly IMediator _mediator;

    public UpdateOrderEngageDepartmentsCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationStatus> Handle(UpdateOrderEngageDepartmentsCommand command, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new BatchAssignCommand
        {
            Mapping = AssignDesc.ENGAGE_DEPARTMENT_ORDER,
            ToId = command.Id,
            AssignedIds = command.EngageDepartmentIds
        });
    }
}

public class UpdateOrderDateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateOrderDateCommand, OperationStatus>
{
    public UpdateOrderDateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }
    public async Task<OperationStatus> Handle(UpdateOrderDateCommand command, CancellationToken cancellationToken)
    {
        var entity = await OrderUtils.GetOrder(_context, command.Id);
        entity.OrderDate = command.OrderDate;
        return await SaveChangesAsync(command.Id, cancellationToken);
    }
}

public class UpdateOrderDeliveryDateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateOrderDeliveryDateCommand, OperationStatus>
{
    public UpdateOrderDeliveryDateCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }
    public async Task<OperationStatus> Handle(UpdateOrderDeliveryDateCommand command, CancellationToken cancellationToken)
    {
        var entity = await OrderUtils.GetOrder(_context, command.Id);
        if (command.DeliveryDate >= entity.OrderDate)
        {
            entity.DeliveryDate = command.DeliveryDate;
            return await SaveChangesAsync(command.Id, cancellationToken);
        }
        else
        {
            return new OperationStatus() { Status = false, Exception = true, Message = "Delivery Date can't be before the Order Date" };
        }

    }
}

public class UpdateOrderReferenceCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateOrderReferenceCommand, OperationStatus>
{
    public UpdateOrderReferenceCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }
    public async Task<OperationStatus> Handle(UpdateOrderReferenceCommand command, CancellationToken cancellationToken)
    {
        var entity = await OrderUtils.GetOrder(_context, command.Id);
        entity.OrderReference = command.OrderReference;
        return await SaveChangesAsync(command.Id, cancellationToken);
    }
}
