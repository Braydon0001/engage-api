namespace Engage.Application.Services.StoreAssetFiles.Commands;

public class StoreAssetFileInsertCommand : IMapTo<StoreAssetFile>, IRequest<StoreAssetFile>
{
    public int StoreAssetId { get; init; }
    public string ImageUrl { get; init; }
    public int StoreAssetFileTypeId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetFileInsertCommand, StoreAssetFile>();
    }
}

public record StoreAssetFileInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetFileInsertCommand, StoreAssetFile>
{
    public async Task<StoreAssetFile> Handle(StoreAssetFileInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<StoreAssetFileInsertCommand, StoreAssetFile>(command);
        
        Context.StoreAssetFiles.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class StoreAssetFileInsertValidator : AbstractValidator<StoreAssetFileInsertCommand>
{
    public StoreAssetFileInsertValidator()
    {
        RuleFor(e => e.StoreAssetId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ImageUrl);
        RuleFor(e => e.StoreAssetFileTypeId).NotEmpty().GreaterThan(0);
    }
}