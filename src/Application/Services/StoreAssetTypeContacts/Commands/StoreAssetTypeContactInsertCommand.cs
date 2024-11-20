using Engage.Application.Services.StoreAssetTypeStoreAssetTypeContact.Commands;

namespace Engage.Application.Services.StoreAssetTypeContacts.Commands;

public class StoreAssetTypeContactInsertCommand : IMapTo<StoreAssetTypeContact>, IRequest<StoreAssetTypeContact>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string EmailAddress { get; init; }
    public string MobilePhone { get; init; }
    public List<int> StoreAssetTypeIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetTypeContactInsertCommand, StoreAssetTypeContact>();
    }
}

public record StoreAssetTypeContactInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<StoreAssetTypeContactInsertCommand, StoreAssetTypeContact>
{
    public async Task<StoreAssetTypeContact> Handle(StoreAssetTypeContactInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<StoreAssetTypeContactInsertCommand, StoreAssetTypeContact>(command);

        Context.StoreAssetTypeContacts.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        await Mediator.Send(new StoreAssetTypeStoreAssetTypeContactInsertFromContactCommand
        {
            StoreAssetTypeContactId = entity.StoreAssetTypeContactId,
            StoreAssetTypeIds = command.StoreAssetTypeIds
        }, cancellationToken);

        return entity;
    }
}

public class StoreAssetTypeContactInsertValidator : AbstractValidator<StoreAssetTypeContactInsertCommand>
{
    public StoreAssetTypeContactInsertValidator()
    {
        RuleFor(e => e.FirstName).MaximumLength(120);
        RuleFor(e => e.LastName).MaximumLength(120);
        RuleFor(e => e.EmailAddress).NotEmpty().MaximumLength(120);
        RuleFor(e => e.MobilePhone).MaximumLength(120);
    }
}