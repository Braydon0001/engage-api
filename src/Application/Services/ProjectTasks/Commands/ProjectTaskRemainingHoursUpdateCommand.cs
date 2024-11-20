namespace Engage.Application.Services.ProjectTasks.Commands;

public class ProjectTaskRemainingHoursUpdateCommand : IRequest<ProjectTask>
{
    public int Id { get; set; }
    public float RemainingHours { get; set; }
}
public record ProjectTaskRemainingHoursUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskRemainingHoursUpdateCommand, ProjectTask>
{
    public async Task<ProjectTask> Handle(ProjectTaskRemainingHoursUpdateCommand command, CancellationToken cancellationToken)
    {
        var projectTask = await Context.ProjectTasks.FirstOrDefaultAsync(e => e.ProjectTaskId == command.Id, cancellationToken);

        if (projectTask.EstimatedHours.Value < command.RemainingHours)
        {
            throw new Exception("Remaining hours cannot be greater than estimated hours");
        }
        projectTask.RemainingHours = command.RemainingHours;

        await Context.SaveChangesAsync(cancellationToken);

        return projectTask;
    }
}
public class ProjectTaskRemainingHoursUpdateValidator : AbstractValidator<ProjectTaskRemainingHoursUpdateCommand>
{
    public ProjectTaskRemainingHoursUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.RemainingHours).GreaterThanOrEqualTo(0);
    }
}