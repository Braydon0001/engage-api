namespace Engage.Application.Services.Warehouses.Commands
{
    public class CreateWarehouseCommand : WarehouseCommand, IRequest<OperationStatus>
    {
    }

    public class CreateWarehouseCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateWarehouseCommand, OperationStatus>
    {
        public CreateWarehouseCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OperationStatus> Handle(CreateWarehouseCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateWarehouseCommand, Warehouse>(command);
            _context.Warehouses.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.WarehouseId;
            return opStatus;
        }
    }
}
