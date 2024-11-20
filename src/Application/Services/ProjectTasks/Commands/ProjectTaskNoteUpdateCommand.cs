namespace Engage.Application.Services.ProjectTasks.Commands;

public class ProjectTaskNoteUpdateCommand : IRequest<ProjectTask>
{
    public int Id { get; set; }
    public string Note { get; set; }
}
public record ProjectTaskNoteUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskNoteUpdateCommand, ProjectTask>
{
    public async Task<ProjectTask> Handle(ProjectTaskNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var projectTask = await Context.ProjectTasks.FirstOrDefaultAsync(e => e.ProjectTaskId == command.Id, cancellationToken);

        projectTask.Note = command.Note;

        await Context.SaveChangesAsync(cancellationToken);

        return projectTask;
    }
}
public class ProjectTaskNoteUpdateValidator : AbstractValidator<ProjectTaskNoteUpdateCommand>
{
    public ProjectTaskNoteUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Note).NotEmpty();
    }
}