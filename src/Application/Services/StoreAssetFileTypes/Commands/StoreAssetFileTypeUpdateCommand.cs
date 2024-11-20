namespace Engage.Application.Services.StoreAssetFileTypes.Commands;

public class StoreAssetFileTypeUpdateCommand : IMapTo<StoreAssetFileType>, IRequest<StoreAssetFileType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetFileTypeUpdateCommand, StoreAssetFileType>();
    }
}

public record StoreAssetFileTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetFileTypeUpdateCommand, StoreAssetFileType>
{
    public async Task<StoreAssetFileType> Handle(StoreAssetFileTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.StoreAssetFileTypes.SingleOrDefaultAsync(e => e.StoreAssetFileTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateStoreAssetFileTypeValidator : AbstractValidator<StoreAssetFileTypeUpdateCommand>
{
    public UpdateStoreAssetFileTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
        RuleFor(e => e.Description).MaximumLength(300);
    }
}