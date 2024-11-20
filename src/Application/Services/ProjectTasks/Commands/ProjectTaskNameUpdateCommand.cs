namespace Engage.Application.Services.ProjectTasks.Commands;

public class ProjectTaskNameUpdateCommand : IRequest<ProjectTask>
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public record ProjectTaskNameUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskNameUpdateCommand, ProjectTask>
{
    public async Task<ProjectTask> Handle(ProjectTaskNameUpdateCommand command, CancellationToken cancellationToken)
    {
        var projectTask = await Context.ProjectTasks.FirstOrDefaultAsync(e => e.ProjectTaskId == command.Id, cancellationToken);

        projectTask.Name = command.Name;

        await Context.SaveChangesAsync(cancellationToken);

        return projectTask;
    }
}
public class ProjectTaskNameUpdateValidator : AbstractValidator<ProjectTaskNameUpdateCommand>
{
    public ProjectTaskNameUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty();
    }
}