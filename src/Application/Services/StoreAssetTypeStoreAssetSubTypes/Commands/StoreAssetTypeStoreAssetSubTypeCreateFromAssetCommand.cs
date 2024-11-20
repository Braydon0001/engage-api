namespace Engage.Application.Services.StoreAssetTypeStoreAssetSubTypes.Commands;

public class StoreAssetTypeStoreAssetSubTypeCreateFromAssetCommand : IRequest<OperationStatus>
{
    public int StoreAssetTypeId { get; set; }
    public List<int> StoreAssetSubTypeIds { get; set; }
    public bool Save { get; set; } = true;
}

public record StoreAssetTypeStoreAssetSubTypeCreateFromAssetHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetTypeStoreAssetSubTypeCreateFromAssetCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(StoreAssetTypeStoreAssetSubTypeCreateFromAssetCommand command, CancellationToken cancellationToken)
    {
        var currentTypes = await Context.StoreAssetTypeStoreAssetSubTypes
                                        .Where(e => e.StoreAssetTypeId == command.StoreAssetTypeId)
                                        .ToListAsync(cancellationToken);

        if (currentTypes.Any())
        {
            Context.StoreAssetTypeStoreAssetSubTypes.RemoveRange(currentTypes);
        }

        foreach (var id in command.StoreAssetSubTypeIds)
        {
            Context.StoreAssetTypeStoreAssetSubTypes.Add(new StoreAssetTypeStoreAssetSubType
            {
                StoreAssetSubTypeId = id,
                StoreAssetTypeId = command.StoreAssetTypeId
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
