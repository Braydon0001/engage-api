namespace Engage.Application.Services.StoreAssetFiles.Commands;

public class StoreAssetFileUpdateCommand : IMapTo<StoreAssetFile>, IRequest<StoreAssetFile>
{
    public int Id { get; set; }
    public int StoreAssetId { get; init; }
    public string ImageUrl { get; init; }
    public int StoreAssetFileTypeId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetFileUpdateCommand, StoreAssetFile>();
    }
}

public record StoreAssetFileUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<StoreAssetFileUpdateCommand, StoreAssetFile>
{
    public async Task<StoreAssetFile> Handle(StoreAssetFileUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.StoreAssetFiles.SingleOrDefaultAsync(e => e.StoreAssetFileId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateStoreAssetFileValidator : AbstractValidator<StoreAssetFileUpdateCommand>
{
    public UpdateStoreAssetFileValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreAssetId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ImageUrl);
        RuleFor(e => e.StoreAssetFileTypeId).NotEmpty().GreaterThan(0);
    }
}