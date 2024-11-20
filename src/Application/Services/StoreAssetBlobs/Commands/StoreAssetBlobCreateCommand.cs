namespace Engage.Application.Services.AssetImages.Commands;

public class StoreAssetBlobCreateCommand : StoreAssetBlobCommand, IRequest<OperationStatus>
{
}

public class StoreAssetBlobCreateHandler : BaseCreateCommandHandler, IRequestHandler<StoreAssetBlobCreateCommand, OperationStatus>
{
    public StoreAssetBlobCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(StoreAssetBlobCreateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<StoreAssetBlobCreateCommand, StoreAssetBlob>(command);
        _context.StoreAssetBlobs.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreAssetBlobId;
        opStatus.ReturnObject = command.ImageUrl;
        return opStatus;
    }
}
