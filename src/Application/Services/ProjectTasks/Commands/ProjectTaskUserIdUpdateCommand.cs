namespace Engage.Application.Services.ProjectTasks.Commands;

public class ProjectTaskUserIdUpdateCommand : IRequest<ProjectTask>
{
    public int Id { get; set; }
    public int UserId { get; set; }
}
public record ProjectTaskUserIdUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskUserIdUpdateCommand, ProjectTask>
{
    public async Task<ProjectTask> Handle(ProjectTaskUserIdUpdateCommand command, CancellationToken cancellationToken)
    {
        var projectTask = await Context.ProjectTasks.FirstOrDefaultAsync(e => e.ProjectTaskId == command.Id, cancellationToken);

        projectTask.UserId = command.UserId;

        await Context.SaveChangesAsync(cancellationToken);

        return projectTask;
    }
}
public class ProjectTaskUserIdUpdateValidator : AbstractValidator<ProjectTaskUserIdUpdateCommand>
{
    public ProjectTaskUserIdUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.UserId).NotEmpty();
    }
}