
using Engage.Application.Services.StoreAssetOwnerStoreAssetType.Commands;
using Engage.Application.Services.StoreAssetTypeStoreAssetSubTypes.Commands;
using Engage.Application.Services.StoreAssetTypeStoreAssetTypeContact.Commands;

namespace Engage.Application.Services.StoreAssetTypes.Commands;

public class StoreAssetTypeUpdateCommand : IRequest<StoreAssetType>, IMapTo<StoreAssetType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> StoreAssetOwnerIds { get; set; }
    public List<int> StoreAssetTypeContactIds { get; set; }
    public List<int> StoreAssetSubTypeIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetTypeUpdateCommand, StoreAssetType>();
    }
}
public record StoreAssetTypeUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<StoreAssetTypeUpdateCommand, StoreAssetType>
{
    public async Task<StoreAssetType> Handle(StoreAssetTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.StoreAssetTypes.SingleOrDefaultAsync(e => e.Id == command.Id, cancellationToken);

        if (entity == null)
        {
            return null;
        }

        Mapper.Map(command, entity);

        var ownersSaved = await Mediator.Send(new StoreAssetOwnerStoreAssetTypeCreateCommand
        {
            StoreAssetOwnerIds = command.StoreAssetOwnerIds,
            StoreAssetTypeId = entity.Id,
            Save = false
        }, cancellationToken);



        await Mediator.Send(new StoreAssetTypeStoreAssetTypeContactInsertFromTypeCommand
        {
            StoreAssetTypeId = entity.Id,
            StoreAssetTypeContactIds = command.StoreAssetTypeContactIds,
            Save = false
        }, cancellationToken);

        await Mediator.Send(new StoreAssetTypeStoreAssetSubTypeCreateFromAssetCommand
        {
            StoreAssetTypeId = entity.Id,
            StoreAssetSubTypeIds = command.StoreAssetSubTypeIds,
            Save = false
        }, cancellationToken);


        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
public class StoreAssetTypeUpdateValidator : AbstractValidator<StoreAssetTypeUpdateCommand>
{
    public StoreAssetTypeUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).MaximumLength(200);
    }
}