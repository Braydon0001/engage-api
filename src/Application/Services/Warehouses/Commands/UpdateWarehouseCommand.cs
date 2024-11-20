namespace Engage.Application.Services.Warehouses.Commands;

public class UpdateWarehouseCommand : WarehouseCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateWarehouseCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateWarehouseCommand, OperationStatus>
{
    public UpdateWarehouseCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateWarehouseCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Warehouses.SingleAsync(x => x.WarehouseId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = entity.WarehouseId;
        return operationStatus;
    }
}
