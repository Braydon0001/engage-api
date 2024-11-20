namespace Engage.Application.Services.ProjectFiles.Commands;

public class ProjectFileDeleteCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class ProjectFileDeleteHandler : FileDeleteHandler, IRequestHandler<ProjectFileDeleteCommand, bool>
{
    public ProjectFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(ProjectFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ProjectFiles.SingleOrDefaultAsync(e => e.ProjectFileId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null)
        {
            return false;
        }

        var fileCommand = new FileDeleteCommand
        {
            Id = command.Id,
            FileName = entity.Files[0].Name,
            FileType = entity.Files[0].Type,
        };

        await _file.DeleteAsync(fileCommand, nameof(ProjectFile), cancellationToken);

        _context.ProjectFiles.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class ProjectFileDeleteValidator : AbstractValidator<ProjectFileDeleteCommand>
{
    public ProjectFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}