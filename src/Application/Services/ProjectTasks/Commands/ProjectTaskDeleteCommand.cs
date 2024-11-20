namespace Engage.Application.Services.ProjectTasks.Commands;

public record ProjectTaskDeleteCommand(int Id) : IRequest<ProjectTask>
{
}

public class ProjectTaskDeleteHandler : IRequestHandler<ProjectTaskDeleteCommand, ProjectTask>
{
    private readonly IAppDbContext _context;
    public ProjectTaskDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectTask> Handle(ProjectTaskDeleteCommand query, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectTasks.SingleOrDefaultAsync(e => e.ProjectTaskId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        _context.ProjectTasks.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
