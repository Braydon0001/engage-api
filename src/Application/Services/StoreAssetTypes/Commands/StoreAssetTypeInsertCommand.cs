
using Engage.Application.Services.StoreAssetOwnerStoreAssetType.Commands;
using Engage.Application.Services.StoreAssetTypeStoreAssetSubTypes.Commands;
using Engage.Application.Services.StoreAssetTypeStoreAssetTypeContact.Commands;

namespace Engage.Application.Services.StoreAssetTypes.Commands;

public class StoreAssetTypeInsertCommand : IRequest<StoreAssetType>, IMapTo<StoreAssetType>
{
    public string Name { get; set; }
    public List<int> StoreAssetOwnerIds { get; set; }
    public List<int> StoreAssetTypeContactIds { get; set; }
    public List<int> StoreAssetSubTypeIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetTypeInsertCommand, StoreAssetType>();
    }
}
public record StoreAssetTypeInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<StoreAssetTypeInsertCommand, StoreAssetType>
{
    public async Task<StoreAssetType> Handle(StoreAssetTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<StoreAssetTypeInsertCommand, StoreAssetType>(command);
        Context.StoreAssetTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        if (command.StoreAssetOwnerIds.NotNullOrEmpty())
        {
            await Mediator.Send(new StoreAssetOwnerStoreAssetTypeCreateCommand
            {
                StoreAssetTypeId = entity.Id,
                StoreAssetOwnerIds = command.StoreAssetOwnerIds,
            }, cancellationToken);
        }

        if (command.StoreAssetTypeContactIds.NotNullOrEmpty())
        {
            await Mediator.Send(new StoreAssetTypeStoreAssetTypeContactInsertFromTypeCommand
            {
                StoreAssetTypeId = entity.Id,
                StoreAssetTypeContactIds = command.StoreAssetTypeContactIds
            }, cancellationToken);
        }

        if (command.StoreAssetSubTypeIds.NotNullOrEmpty())
        {
            await Mediator.Send(new StoreAssetTypeStoreAssetSubTypeCreateFromAssetCommand
            {
                StoreAssetTypeId = entity.Id,
                StoreAssetSubTypeIds = command.StoreAssetSubTypeIds,
            }, cancellationToken);
        }

        return entity;
    }
}
public class StoreAssetTypeInsertValidator : AbstractValidator<StoreAssetTypeInsertCommand>
{
    public StoreAssetTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
    }
}