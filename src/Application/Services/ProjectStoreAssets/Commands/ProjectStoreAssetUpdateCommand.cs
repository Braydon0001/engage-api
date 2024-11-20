namespace Engage.Application.Services.ProjectStoreAssets.Commands;

public class ProjectStoreAssetUpdateCommand : IMapTo<ProjectStoreAsset>, IRequest<ProjectStoreAsset>
{
    public int Id { get; set; }
    public int ProjectId { get; init; }
    public int StoreAssetId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStoreAssetUpdateCommand, ProjectStoreAsset>();
    }
}

public record ProjectStoreAssetUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStoreAssetUpdateCommand, ProjectStoreAsset>
{
    public async Task<ProjectStoreAsset> Handle(ProjectStoreAssetUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectStoreAssets.SingleOrDefaultAsync(e => e.ProjectStoreAssetId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectStoreAssetValidator : AbstractValidator<ProjectStoreAssetUpdateCommand>
{
    public UpdateProjectStoreAssetValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreAssetId).NotEmpty().GreaterThan(0);
    }
}