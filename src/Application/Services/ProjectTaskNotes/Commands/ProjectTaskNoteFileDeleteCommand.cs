namespace Engage.Application.Services.ProjectTaskNotes.Commands;

public class ProjectTaskNoteFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record ProjectTaskNoteFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProjectTaskNoteFileDeleteCommand, bool>
{
    public async Task<bool> Handle(ProjectTaskNoteFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTaskNotes.SingleOrDefaultAsync(e => e.ProjectTaskNoteId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(ProjectTaskNote), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class ProjectTaskNoteFileDeleteValidator : FileDeleteValidator<ProjectTaskNoteFileDeleteCommand>
{
    public ProjectTaskNoteFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}