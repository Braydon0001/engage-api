namespace Engage.Application.Services.StoreAssetFileTypes.Commands;

public class StoreAssetFileTypeInsertCommand : IMapTo<StoreAssetFileType>, IRequest<StoreAssetFileType>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetFileTypeInsertCommand, StoreAssetFileType>();
    }
}

public record StoreAssetFileTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetFileTypeInsertCommand, StoreAssetFileType>
{
    public async Task<StoreAssetFileType> Handle(StoreAssetFileTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<StoreAssetFileTypeInsertCommand, StoreAssetFileType>(command);

        Context.StoreAssetFileTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class StoreAssetFileTypeInsertValidator : AbstractValidator<StoreAssetFileTypeInsertCommand>
{
    public StoreAssetFileTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
        RuleFor(e => e.Description).MaximumLength(300);
    }
}