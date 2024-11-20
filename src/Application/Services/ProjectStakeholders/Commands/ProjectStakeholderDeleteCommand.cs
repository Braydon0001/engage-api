namespace Engage.Application.Services.ProjectStakeholders.Commands;

public record ProjectStakeholderDeleteCommand(int Id) : IRequest<OperationStatus>
{
}

public class ProjectStakeholderDeleteHandler : IRequestHandler<ProjectStakeholderDeleteCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    public ProjectStakeholderDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(ProjectStakeholderDeleteCommand query, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectStakeholders.SingleOrDefaultAsync(e => e.ProjectStakeholderId == query.Id, cancellationToken);

        if (entity == null)
        {
            return null;
        }

        if (entity is ProjectStakeholderUser)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(e => e.ProjectId == entity.ProjectId, cancellationToken)
                    ?? throw new Exception("Cannot find incident");

            var user = entity as ProjectStakeholderUser;
            if (project.OwnerId.HasValue && project.OwnerId.Value == user.UserId)
            {
                throw new Exception("Cannot remove owner as stakeholder");
            }
        }

        var assigned = await _context.ProjectAssignees.FirstOrDefaultAsync(e => e.ProjectStakeholderId == entity.ProjectStakeholderId, cancellationToken);

        if (assigned != null)
        {
            throw new Exception("Cannot remove stakeholder that is assigned to incident");
        }


        _context.ProjectStakeholders.Remove(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}
