namespace Engage.Application.Services.AssetImages.Commands;

public class StoreAssetBlobUpdateCommand : StoreAssetBlobCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class StoreeAssetBlobUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<StoreAssetBlobUpdateCommand, OperationStatus>
{
    public StoreeAssetBlobUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(StoreAssetBlobUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreAssetBlobs.SingleAsync(x => x.StoreAssetBlobId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreAssetId;
        return opStatus;
    }
}
