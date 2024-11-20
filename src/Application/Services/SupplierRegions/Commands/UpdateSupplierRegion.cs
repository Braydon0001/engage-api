namespace Engage.Application.Services.SupplierRegions.Commands;

public class UpdateSupplierRegionCommand : SupplierRegionCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateAssetCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateSupplierRegionCommand, OperationStatus>
{
    public UpdateAssetCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(UpdateSupplierRegionCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierRegions.SingleAsync(x => x.Id == command.Id);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}
