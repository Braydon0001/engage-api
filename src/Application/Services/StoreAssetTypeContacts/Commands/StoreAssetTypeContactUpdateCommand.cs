using Engage.Application.Services.StoreAssetTypeStoreAssetTypeContact.Commands;

namespace Engage.Application.Services.StoreAssetTypeContacts.Commands;

public class StoreAssetTypeContactUpdateCommand : IMapTo<StoreAssetTypeContact>, IRequest<StoreAssetTypeContact>
{
    public int Id { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string EmailAddress { get; init; }
    public string MobilePhone { get; init; }
    public List<int> StoreAssetTypeIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetTypeContactUpdateCommand, StoreAssetTypeContact>();
    }
}

public record StoreAssetTypeContactUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<StoreAssetTypeContactUpdateCommand, StoreAssetTypeContact>
{
    public async Task<StoreAssetTypeContact> Handle(StoreAssetTypeContactUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.StoreAssetTypeContacts.SingleOrDefaultAsync(e => e.StoreAssetTypeContactId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Mediator.Send(new StoreAssetTypeStoreAssetTypeContactInsertFromContactCommand
        {
            StoreAssetTypeContactId = entity.StoreAssetTypeContactId,
            StoreAssetTypeIds = command.StoreAssetTypeIds,
            Save = false
        }, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateStoreAssetTypeContactValidator : AbstractValidator<StoreAssetTypeContactUpdateCommand>
{
    public UpdateStoreAssetTypeContactValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.FirstName).MaximumLength(120);
        RuleFor(e => e.LastName).MaximumLength(120);
        RuleFor(e => e.EmailAddress).NotEmpty().MaximumLength(120);
        RuleFor(e => e.MobilePhone).MaximumLength(120);
    }
}