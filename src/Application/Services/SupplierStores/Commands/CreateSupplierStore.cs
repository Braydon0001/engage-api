namespace Engage.Application.Services.SupplierStores.Commands;

public class CreateSupplierStoreCommand : SupplierStoreCommand, IRequest<OperationStatus>
{
}

public class CreateSupplierStoreCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateSupplierStoreCommand, OperationStatus>
{
    public CreateSupplierStoreCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateSupplierStoreCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateSupplierStoreCommand, SupplierStore>(command);
        _context.SupplierStores.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.SupplierStoreId;
        return opStatus;
    }
}
