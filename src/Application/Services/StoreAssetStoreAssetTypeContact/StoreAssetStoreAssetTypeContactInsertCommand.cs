namespace Engage.Application.Services.StoreAssetStoreAssetTypeContact;

public class StoreAssetStoreAssetTypeContactInsertCommand : IRequest<bool>
{
    public int StoreAssetId { get; set; }
    public List<int> StoreAssetTypeContactIds { get; set; }
    public bool Save { get; set; } = true;
}
public record StoreAssetStoreAssetTypeContactInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetStoreAssetTypeContactInsertCommand, bool>
{
    public async Task<bool> Handle(StoreAssetStoreAssetTypeContactInsertCommand command, CancellationToken cancellationToken)
    {
        var manyToMany = await Context.StoreAssetStoreAssetTypeContacts.Where(e => e.StoreAssetId == command.StoreAssetId)
                                                                       .ToListAsync(cancellationToken);

        if (manyToMany.Any())
        {
            Context.StoreAssetStoreAssetTypeContacts.RemoveRange(manyToMany);
        }

        foreach (var contactId in command.StoreAssetTypeContactIds)
        {
            Context.StoreAssetStoreAssetTypeContacts.Add(
                new Domain.Entities.StoreAssetStoreAssetTypeContact
                {
                    StoreAssetId = command.StoreAssetId,
                    StoreAssetTypeContactId = contactId
                });
        }

        if (command.Save)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        return true;
    }
}