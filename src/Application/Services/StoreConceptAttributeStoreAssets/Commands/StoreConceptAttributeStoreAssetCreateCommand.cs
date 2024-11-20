namespace Engage.Application.Services.StoreConceptAttributeStoreAssets.Commands;

public class StoreConceptAttributeStoreAssetCreateCommand : StoreConceptAttributeStoreAssetCommand, IRequest<OperationStatus>
{
}

public class StoreConceptAttibuteStoreAssetCreateCommandHandler : BaseCreateCommandHandler, IRequestHandler<StoreConceptAttributeStoreAssetCreateCommand, OperationStatus>
{
    public StoreConceptAttibuteStoreAssetCreateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeStoreAssetCreateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<StoreConceptAttributeStoreAssetCreateCommand, StoreConceptAttributeStoreAsset>(command);
        _context.StoreConceptAttributeStoreAssets.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreConceptAttributeId;
        return opStatus;
    }
}
