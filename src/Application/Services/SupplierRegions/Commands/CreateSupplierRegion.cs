namespace Engage.Application.Services.SupplierRegions.Commands;

public class CreateSupplierRegionCommand : SupplierRegionCommand, IRequest<OperationStatus>
{
}

public class CreateAssetCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateSupplierRegionCommand, OperationStatus>
{
    public CreateAssetCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateSupplierRegionCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateSupplierRegionCommand, SupplierRegion>(command);
        _context.SupplierRegions.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}
