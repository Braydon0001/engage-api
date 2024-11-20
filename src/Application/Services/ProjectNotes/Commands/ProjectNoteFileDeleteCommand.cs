namespace Engage.Application.Services.ProjectNotes.Commands;

public class ProjectNoteFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record ProjectNoteFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProjectNoteFileDeleteCommand, bool>
{
    public async Task<bool> Handle(ProjectNoteFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectNotes.SingleOrDefaultAsync(e => e.ProjectNoteId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(ProjectNote), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        await Context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class ProjectNoteFileDeleteValidator : FileDeleteValidator<ProjectNoteFileDeleteCommand>
{
    public ProjectNoteFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}