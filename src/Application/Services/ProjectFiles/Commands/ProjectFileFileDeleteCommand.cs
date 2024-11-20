namespace Engage.Application.Services.ProjectFiles.Commands;

public class ProjectFileFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class ProjectFileFileDeleteHandler : FileDeleteHandler, IRequestHandler<ProjectFileFileDeleteCommand, bool>
{
    public ProjectFileFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(ProjectFileFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectFiles.SingleOrDefaultAsync(e => e.ProjectFileId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(ProjectFile), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class ProjectFileFileDeleteValidator : FileDeleteValidator<ProjectFileFileDeleteCommand>
{
    public ProjectFileFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}