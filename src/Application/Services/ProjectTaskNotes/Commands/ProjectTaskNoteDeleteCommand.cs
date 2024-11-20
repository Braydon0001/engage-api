namespace Engage.Application.Services.ProjectTaskNotes.Commands;

public record ProjectTaskNoteDeleteCommand(int Id) : IRequest<ProjectTaskNote>
{
}

public class ProjectTaskNoteDeleteHandler : IRequestHandler<ProjectTaskNoteDeleteCommand, ProjectTaskNote>
{
    private readonly IAppDbContext _context;
    public ProjectTaskNoteDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectTaskNote> Handle(ProjectTaskNoteDeleteCommand query, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectTaskNotes.SingleOrDefaultAsync(e => e.ProjectTaskNoteId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        _context.ProjectTaskNotes.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
