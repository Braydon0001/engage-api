namespace Engage.Application.Services.StoreAssetStatuses.Commands;

public class StoreAssetStatusInsertCommand : IMapTo<StoreAssetStatus>, IRequest<StoreAssetStatus>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetStatusInsertCommand, StoreAssetStatus>();
    }
}

public record StoreAssetStatusInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetStatusInsertCommand, StoreAssetStatus>
{
    public async Task<StoreAssetStatus> Handle(StoreAssetStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<StoreAssetStatusInsertCommand, StoreAssetStatus>(command);
        
        Context.StoreAssetStatuses.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class StoreAssetStatusInsertValidator : AbstractValidator<StoreAssetStatusInsertCommand>
{
    public StoreAssetStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
    }
}