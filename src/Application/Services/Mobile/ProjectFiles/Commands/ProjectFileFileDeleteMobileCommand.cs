namespace Engage.Application.Services.Mobile.ProjectFiles.Commands;

public class ProjectFileFileDeleteMobileCommand : FileDeleteCommand, IRequest<bool>
{
}

public class ProjectFileFileDeleteMobileHandler : FileDeleteHandler, IRequestHandler<ProjectFileFileDeleteMobileCommand, bool>
{
    public ProjectFileFileDeleteMobileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(ProjectFileFileDeleteMobileCommand command, CancellationToken cancellationToken)
    {
        var entities = await _context.ProjectFiles.Where(e => e.ProjectId == command.Id).ToListAsync(cancellationToken);

        var entity = entities.Where(e => e.Files[0].Name.Equals(command.FileName, StringComparison.CurrentCultureIgnoreCase) && e.Files[0].Type.Equals(command.FileType, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

        if (entity == null || entity.Files == null)
        {
            return false;
        }

        var deleteCommand = new FileDeleteCommand
        {
            Id = entity.ProjectFileId,
            FileName = entity.Files[0].Name,
            FileType = entity.Files[0].Type
        };

        await _file.DeleteAsync(command, nameof(ProjectFile), cancellationToken);

        _context.ProjectFiles.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class ProjectFileFileDeleteMobileValidator : FileDeleteValidator<ProjectFileFileDeleteMobileCommand>
{
    public ProjectFileFileDeleteMobileValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.FileName).NotEmpty();
    }
}