using Engage.Application.Services.StoreAssetStoreAssetTypeContact;

namespace Engage.Application.Services.StoreAssets.Commands;

public class StoreAssetCreateCommand : StoreAssetCommand, IMapTo<StoreAsset>, IRequest<OperationStatus>
{
}

public class StoreAssetCreateHandler : BaseCreateCommandHandler, IRequestHandler<StoreAssetCreateCommand, OperationStatus>
{
    public StoreAssetCreateHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(StoreAssetCreateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<StoreAssetCreateCommand, StoreAsset>(command);
        _context.StoreAssets.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status == true)
        {
            opStatus.OperationId = entity.StoreAssetId;
            if (command.StoreAssetTypeContactIds != null)
            {
                await _mediator.Send(new StoreAssetStoreAssetTypeContactInsertCommand
                {
                    StoreAssetId = entity.StoreAssetId,
                    StoreAssetTypeContactIds = command.StoreAssetTypeContactIds
                }, cancellationToken);
            }
        }

        return opStatus;
    }
}

public class StoreAssetCreateValidator : StoreAssetValidator<StoreAssetCreateCommand>
{
    public StoreAssetCreateValidator()
    {
    }
}