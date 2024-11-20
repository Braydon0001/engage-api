namespace Engage.Application.Services.StoreAssetTypeStoreAssetTypeContact.Commands;

public class StoreAssetTypeStoreAssetTypeContactInsertFromTypeCommand : IRequest<bool>
{
    public int StoreAssetTypeId { get; set; }
    public List<int> StoreAssetTypeContactIds { get; set; }
    public bool Save { get; set; } = true;
}
public record StoreAssetTypeStoreAssetTypeContactInsertFromTypeHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetTypeStoreAssetTypeContactInsertFromTypeCommand, bool>
{
    public async Task<bool> Handle(StoreAssetTypeStoreAssetTypeContactInsertFromTypeCommand command, CancellationToken cancellationToken)
    {
        var manyToMany = await Context.StoreAssetTypeStoreAssetTypeContacts
                                      .Where(e => e.StoreAssetTypeId == command.StoreAssetTypeId)
                                      .ToListAsync(cancellationToken);

        if (manyToMany.Count > 0)
        {
            Context.StoreAssetTypeStoreAssetTypeContacts.RemoveRange(manyToMany);
        }

        foreach (var contactId in command.StoreAssetTypeContactIds)
        {
            Context.StoreAssetTypeStoreAssetTypeContacts.Add(
                new Domain.Entities.StoreAssetTypeStoreAssetTypeContact
                {
                    StoreAssetTypeId = command.StoreAssetTypeId,
                    StoreAssetTypeContactId = contactId,
                });
        }

        if (command.Save)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        return true;
    }
}

public class StoreAssetTypeStoreAssetTypeContactInsertFromTypeValidator : AbstractValidator<StoreAssetTypeStoreAssetTypeContactInsertFromTypeCommand>
{
    public StoreAssetTypeStoreAssetTypeContactInsertFromTypeValidator()
    {
        RuleFor(e => e.StoreAssetTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreAssetTypeContactIds);
    }
}