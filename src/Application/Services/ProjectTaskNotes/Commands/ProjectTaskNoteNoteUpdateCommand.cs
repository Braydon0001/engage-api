namespace Engage.Application.Services.ProjectTaskNotes.Commands;

public class ProjectTaskNoteNoteUpdateCommand : IRequest<ProjectTaskNote>
{
    public int Id { get; set; }
    public string Note { get; set; }
}
public record ProjectTaskNoteNoteUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectTaskNoteNoteUpdateCommand, ProjectTaskNote>
{
    public async Task<ProjectTaskNote> Handle(ProjectTaskNoteNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var projectTaskNote = await Context.ProjectTaskNotes.FirstOrDefaultAsync(e => e.ProjectTaskNoteId == command.Id, cancellationToken);

        projectTaskNote.Note = command.Note;

        await Context.SaveChangesAsync(cancellationToken);

        return projectTaskNote;
    }
}
public class ProjectTaskNoteNoteUpdateValidator : AbstractValidator<ProjectTaskNoteNoteUpdateCommand>
{
    public ProjectTaskNoteNoteUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Note).NotEmpty();
    }
}