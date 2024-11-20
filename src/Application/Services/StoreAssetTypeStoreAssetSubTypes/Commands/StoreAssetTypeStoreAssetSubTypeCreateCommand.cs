namespace Engage.Application.Services.StoreAssetTypeStoreAssetSubTypes.Commands;

public class StoreAssetTypeStoreAssetSubTypeCreateCommand : IRequest<OperationStatus>
{
    public int StoreAssetSubTypeId { get; set; }
    public List<int> StoreAssetTypeIds { get; set; }
    public bool Save { get; set; } = true;
}
public record StoreAssetTypeStoreAssetSubTypeCreateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetTypeStoreAssetSubTypeCreateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(StoreAssetTypeStoreAssetSubTypeCreateCommand command, CancellationToken cancellationToken)
    {
        var currentTypes = await Context.StoreAssetTypeStoreAssetSubTypes
                                        .Where(e => e.StoreAssetSubTypeId == command.StoreAssetSubTypeId)
                                        .ToListAsync(cancellationToken);

        if (currentTypes.Any())
        {
            Context.StoreAssetTypeStoreAssetSubTypes.RemoveRange(currentTypes);
        }

        foreach (var id in command.StoreAssetTypeIds)
        {
            Context.StoreAssetTypeStoreAssetSubTypes.Add(new StoreAssetTypeStoreAssetSubType
            {
                StoreAssetSubTypeId = command.StoreAssetSubTypeId,
                StoreAssetTypeId = id
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
    }
}
