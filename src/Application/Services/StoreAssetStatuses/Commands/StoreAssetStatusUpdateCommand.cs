namespace Engage.Application.Services.StoreAssetStatuses.Commands;

public class StoreAssetStatusUpdateCommand : IMapTo<StoreAssetStatus>, IRequest<StoreAssetStatus>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetStatusUpdateCommand, StoreAssetStatus>();
    }
}

public record StoreAssetStatusUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetStatusUpdateCommand, StoreAssetStatus>
{
    public async Task<StoreAssetStatus> Handle(StoreAssetStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.StoreAssetStatuses.SingleOrDefaultAsync(e => e.StoreAssetStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateStoreAssetStatusValidator : AbstractValidator<StoreAssetStatusUpdateCommand>
{
    public UpdateStoreAssetStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
    }
}