namespace Engage.Application.Services.ProjectAssignees.Commands;

public class ProjectAssigneeUpdateCommand : IRequest<OperationStatus>
{
    public int ProjectId { get; init; }
    public List<int> ProjectAssignedIds { get; set; }
    public bool Save { get; init; } = true;
}

public record ProjectAssigneeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectAssigneeUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectAssigneeUpdateCommand command, CancellationToken cancellationToken)
    {
        var project = await Context.ProjectStores.FirstOrDefaultAsync(e => e.ProjectId == command.ProjectId, cancellationToken) ?? throw new Exception("No Project Found");

        var assigned = await Context.ProjectAssignees.Where(e => e.ProjectId == command.ProjectId).ToListAsync(cancellationToken);

        var assigneesToDelete = assigned.Where(e => !command.ProjectAssignedIds.Contains(e.ProjectStakeholderId)).ToList();

        var assigneesToAdd = command.ProjectAssignedIds.Where(e => !assigned.Select(e => e.ProjectStakeholderId).ToList().Contains(e)).ToList();

        if (assigneesToDelete.Any())
        {
            Context.ProjectAssignees.RemoveRange(assigneesToDelete);
        }

        if (assigneesToAdd.Any())
        {
            Context.ProjectAssignees.AddRange(assigneesToAdd.Select(e => new ProjectAssignee
            {
                ProjectId = command.ProjectId,
                ProjectStakeholderId = e
            }));
        }

        OperationStatus opStatus = new();

        if (command.Save)
        {
            opStatus = await Context.SaveChangesAsync(cancellationToken);
        }

        return opStatus;
    }
}

public class UpdateProjectAssigneeValidator : AbstractValidator<ProjectAssigneeUpdateCommand>
{
    public UpdateProjectAssigneeValidator()
    {
        RuleFor(e => e.ProjectId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ProjectAssignedIds).NotEmpty();
    }
}