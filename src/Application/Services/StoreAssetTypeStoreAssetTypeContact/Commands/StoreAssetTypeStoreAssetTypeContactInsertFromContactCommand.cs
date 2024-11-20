namespace Engage.Application.Services.StoreAssetTypeStoreAssetTypeContact.Commands;

public class StoreAssetTypeStoreAssetTypeContactInsertFromContactCommand : IRequest<bool>
{
    public int StoreAssetTypeContactId { get; set; }
    public List<int> StoreAssetTypeIds { get; set; }
    public bool Save { get; set; } = true;
}
public record StoreAssetTypeStoreAssetTypeContactInsertFromContactHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetTypeStoreAssetTypeContactInsertFromContactCommand, bool>
{
    public async Task<bool> Handle(StoreAssetTypeStoreAssetTypeContactInsertFromContactCommand command, CancellationToken cancellationToken)
    {
        var manyToMany = await Context.StoreAssetTypeStoreAssetTypeContacts
                                    .Where(e => e.StoreAssetTypeContactId == command.StoreAssetTypeContactId)
                                    .ToListAsync(cancellationToken);

        if (manyToMany.Any())
        {
            Context.StoreAssetTypeStoreAssetTypeContacts.RemoveRange(manyToMany);
        }

        foreach (var typeId in command.StoreAssetTypeIds)
        {
            Context.StoreAssetTypeStoreAssetTypeContacts.Add(
                new Domain.Entities.StoreAssetTypeStoreAssetTypeContact
                {
                    StoreAssetTypeContactId = command.StoreAssetTypeContactId,
                    StoreAssetTypeId = typeId,
                });
        }

        if (command.Save)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        return true;
    }
}

public class StoreAssetTypeStoreAssetTypeContactInsertFromContactValidator : AbstractValidator<StoreAssetTypeStoreAssetTypeContactInsertFromContactCommand>
{
    public StoreAssetTypeStoreAssetTypeContactInsertFromContactValidator()
    {
        RuleFor(e => e.StoreAssetTypeContactId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreAssetTypeIds);
    }
}