namespace Engage.Application.Services.ProjectNotes.Commands;

public record ProjectNoteDeleteCommand(int Id) : IRequest<ProjectNote>
{
}

public class ProjectNoteDeleteHandler : IRequestHandler<ProjectNoteDeleteCommand, ProjectNote>
{
    private readonly IAppDbContext _context;
    public ProjectNoteDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectNote> Handle(ProjectNoteDeleteCommand query, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectNotes.SingleOrDefaultAsync(e => e.ProjectNoteId == query.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        _context.ProjectNotes.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
