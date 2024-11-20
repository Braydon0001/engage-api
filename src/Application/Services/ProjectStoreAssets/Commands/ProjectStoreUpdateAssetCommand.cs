namespace Engage.Application.Services.ProjectStoreAssets.Commands;

public class ProjectStoreUpdateAssetCommand : IRequest<OperationStatus>
{
    public int ProjectId { get; init; }
    public List<int> StoreAssetIds { get; init; }
    public bool Save { get; init; } = true;
}

public record ProjectStoreUpdateAssetHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectStoreUpdateAssetCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectStoreUpdateAssetCommand command, CancellationToken cancellationToken)
    {
        var project = await Context.ProjectStores.FirstOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken) ?? throw new Exception("No Project Found");

        var projectStoreAssets = await Context.ProjectStoreAssets.Where(e => e.ProjectId == command.ProjectId).ToListAsync(cancellationToken);

        var assetsToDelete = projectStoreAssets.Where(e => !command.StoreAssetIds.Contains(e.StoreAssetId)).ToList();

        var assetsToAdd = command.StoreAssetIds.Where(e => !projectStoreAssets.Select(e => e.StoreAssetId).ToList().Contains(e)).ToList();

        if (assetsToDelete.Any())
        {
            Context.ProjectStoreAssets.RemoveRange(assetsToDelete);
        }

        if (assetsToAdd.Any())
        {
            Context.ProjectStoreAssets.AddRange(assetsToAdd.Select(e => new ProjectStoreAsset
            {
                ProjectId = command.ProjectId,
                StoreAssetId = e
            }).ToList());
        }

        OperationStatus status = new();

        if (command.Save)
        {
            status = await Context.SaveChangesAsync(cancellationToken);
        }

        return status;
    }
}

public class ProjectStoreUpdateAssetValidator : AbstractValidator<ProjectStoreUpdateAssetCommand>
{
    public ProjectStoreUpdateAssetValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreAssetIds).NotNull();
        //RuleForEach(e => e.StoreAssetIds).GreaterThan(0);
    }
}