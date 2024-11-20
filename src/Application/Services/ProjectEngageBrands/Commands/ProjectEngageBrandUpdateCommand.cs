namespace Engage.Application.Services.ProjectEngageBrands.Commands;

public class ProjectEngageBrandUpdateCommand : IMapTo<ProjectEngageBrand>, IRequest<OperationStatus>
{
    public int ProjectId { get; init; }
    public List<int> EngageBrandIds { get; init; }
    public bool Save { get; init; } = true;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectEngageBrandUpdateCommand, ProjectEngageBrand>();
    }
}

public record ProjectEngageBrandUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectEngageBrandUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectEngageBrandUpdateCommand command, CancellationToken cancellationToken)
    {
        var project = await Context.ProjectStores.FirstOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken) ?? throw new Exception("No Project Found");
        var projectBrands = await Context.ProjectEngageBrands.Where(e => e.ProjectId == command.ProjectId).ToListAsync(cancellationToken);

        var projectBrandsToDelete = projectBrands.Where(e => !command.EngageBrandIds.Contains(e.EngageBrandId)).ToList();

        var projectBrandsToAdd = command.EngageBrandIds.Where(e => !projectBrands.Select(e => e.EngageBrandId).Contains(e)).ToList();

        if (projectBrandsToDelete.Any())
        {
            Context.ProjectEngageBrands.RemoveRange(projectBrandsToDelete);
        }

        if (projectBrandsToAdd.Any())
        {
            Context.ProjectEngageBrands.AddRange(projectBrandsToAdd.Select(e => new ProjectEngageBrand
            {
                ProjectId = project.ProjectId,
                EngageBrandId = e
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

public class UpdateProjectEngageBrandValidator : AbstractValidator<ProjectEngageBrandUpdateCommand>
{
    public UpdateProjectEngageBrandValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EngageBrandIds).NotNull();
    }
}