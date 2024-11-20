namespace Engage.Application.Services.ProjectNotes.Commands;

public class ProjectNoteNoteUpdateCommand : IRequest<ProjectNote>
{
    public int Id { get; set; }
    public string Note { get; set; }
}
public record ProjectNoteNoteUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectNoteNoteUpdateCommand, ProjectNote>
{
    public async Task<ProjectNote> Handle(ProjectNoteNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var ProjectNote = await Context.ProjectNotes.FirstOrDefaultAsync(e => e.ProjectNoteId == command.Id, cancellationToken);

        ProjectNote.Note = command.Note;

        await Context.SaveChangesAsync(cancellationToken);

        return ProjectNote;
    }
}
public class ProjectNoteNoteUpdateValidator : AbstractValidator<ProjectNoteNoteUpdateCommand>
{
    public ProjectNoteNoteUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Note).NotEmpty();
    }
}