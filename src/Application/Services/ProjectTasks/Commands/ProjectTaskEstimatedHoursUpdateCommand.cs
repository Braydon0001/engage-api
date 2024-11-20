namespace Engage.Application.Services.ProjectTasks.Commands;

public class ProjectTaskEstimatedHoursUpdateCommand : IRequest<ProjectTask>
{
    public int Id { get; set; }
    public float EstimatedHours { get; set; }
}
public record ProjectTaskEstimatedHoursUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskEstimatedHoursUpdateCommand, ProjectTask>
{
    public async Task<ProjectTask> Handle(ProjectTaskEstimatedHoursUpdateCommand command, CancellationToken cancellationToken)
    {
        var projectTask = await Context.ProjectTasks.FirstOrDefaultAsync(e => e.ProjectTaskId == command.Id, cancellationToken);

        projectTask.EstimatedHours = command.EstimatedHours;
        projectTask.RemainingHours = command.EstimatedHours;

        await Context.SaveChangesAsync(cancellationToken);

        return projectTask;
    }
}
public class ProjectTaskEstimatedHoursUpdateValidator : AbstractValidator<ProjectTaskEstimatedHoursUpdateCommand>
{
    public ProjectTaskEstimatedHoursUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EstimatedHours).NotEmpty();
    }
}