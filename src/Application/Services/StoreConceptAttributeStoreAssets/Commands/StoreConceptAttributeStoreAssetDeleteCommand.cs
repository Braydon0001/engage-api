namespace Engage.Application.Services.StoreConceptAttributeStoreAssets.Commands;

public class StoreConceptAttributeStoreAssetDeleteCommand : StoreConceptAttributeStoreAssetCommand, IRequest<OperationStatus>
{
}

public class StoreConceptAttributeStoreAssetDeleteCommandHandler : IRequestHandler<StoreConceptAttributeStoreAssetDeleteCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public StoreConceptAttributeStoreAssetDeleteCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(StoreConceptAttributeStoreAssetDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreConceptAttributeStoreAssets.SingleOrDefaultAsync(x => x.StoreConceptAttributeId == command.StoreConceptAttributeId && x.StoreAssetId == command.StoreAssetId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        _context.StoreConceptAttributeStoreAssets.Remove(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}