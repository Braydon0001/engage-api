namespace Engage.Application.Services.ProjectDcProducts.Commands;

public class ProjectUpdateDcProductCommand : IRequest<OperationStatus>
{
    public int ProjectId { get; init; }
    public List<int> DcProductIds { get; init; }
    public bool Save { get; init; } = true;
}

public record ProjectUpdateDcProductCommandHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectUpdateDcProductCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectUpdateDcProductCommand command, CancellationToken cancellationToken)
    {
        var project = await Context.ProjectStores.FirstOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken) ?? throw new Exception("No Project Found");

        var projectDcProducts = await Context.ProjectDcProducts.Where(e => e.ProjectId == command.ProjectId).ToListAsync(cancellationToken);

        var assetsToDelete = projectDcProducts.Where(e => !command.DcProductIds.Contains(e.DcProductId)).ToList();

        var assetsToAdd = command.DcProductIds.Where(e => !projectDcProducts.Select(e => e.DcProductId).Contains(e)).ToList();

        if (assetsToDelete.Any())
        {
            Context.ProjectDcProducts.RemoveRange(assetsToDelete);
        }

        if (assetsToAdd.Any())
        {
            Context.ProjectDcProducts.AddRange(assetsToAdd.Select(e => new ProjectDcProduct
            {
                ProjectId = command.ProjectId,
                DcProductId = e
            }));
        }

        OperationStatus status = new();

        if (command.Save)
        {
            status = await Context.SaveChangesAsync(cancellationToken);
        }

        return status;
    }
}

public class ProjectUpdateDcProductValidator : AbstractValidator<ProjectUpdateDcProductCommand>
{
    public ProjectUpdateDcProductValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.DcProductIds).NotNull();
    }
}