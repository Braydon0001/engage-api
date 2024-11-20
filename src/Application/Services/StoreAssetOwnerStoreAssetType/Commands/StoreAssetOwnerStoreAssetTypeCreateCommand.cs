namespace Engage.Application.Services.StoreAssetOwnerStoreAssetType.Commands;

public class StoreAssetOwnerStoreAssetTypeCreateCommand : IRequest<OperationStatus>
{
    public int StoreAssetTypeId { get; set; }
    public List<int> StoreAssetOwnerIds { get; set; }
    public bool Save { get; set; } = true;
}
public record StoreAssetOwnerStoreAssetTypeCreateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetOwnerStoreAssetTypeCreateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(StoreAssetOwnerStoreAssetTypeCreateCommand command, CancellationToken cancellationToken)
    {
        var currentOwners = await Context.StoreAssetOwnerStoreAssetTypes
                                         .Where(x => x.StoreAssetTypeId == command.StoreAssetTypeId)
                                         .ToListAsync(cancellationToken);

        if (currentOwners.Any())
        {
            Context.StoreAssetOwnerStoreAssetTypes.RemoveRange(currentOwners);
        }

        foreach (var ownerId in command.StoreAssetOwnerIds)
        {
            Context.StoreAssetOwnerStoreAssetTypes.Add(new Domain.Entities.StoreAssetOwnerStoreAssetType
            {
                StoreAssetTypeId = command.StoreAssetTypeId,
                StoreAssetOwnerId = ownerId
            });
        }

        if (command.Save)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            return new();
        }
        throw new NotImplementedException();
    }
}
