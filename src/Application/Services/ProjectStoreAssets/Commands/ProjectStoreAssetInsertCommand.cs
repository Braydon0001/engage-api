namespace Engage.Application.Services.ProjectStoreAssets.Commands;

public class ProjectStoreAssetInsertCommand : IMapTo<ProjectStoreAsset>, IRequest<ProjectStoreAsset>
{
    public int ProjectId { get; init; }
    public int StoreAssetId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStoreAssetInsertCommand, ProjectStoreAsset>();
    }
}

public record ProjectStoreAssetInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStoreAssetInsertCommand, ProjectStoreAsset>
{
    public async Task<ProjectStoreAsset> Handle(ProjectStoreAssetInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<ProjectStoreAssetInsertCommand, ProjectStoreAsset>(command);
        
        Context.ProjectStoreAssets.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class ProjectStoreAssetInsertValidator : AbstractValidator<ProjectStoreAssetInsertCommand>
{
    public ProjectStoreAssetInsertValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreAssetId).NotEmpty().GreaterThan(0);
    }
}