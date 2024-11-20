namespace Engage.Application.Services.Projects.Commands;

public class ProjectFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record ProjectFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProjectFileDeleteCommand, bool>
{
    public async Task<bool> Handle(ProjectFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Projects.SingleOrDefaultAsync(e => e.ProjectId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(Project), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class ProjectFileDeleteValidator : FileDeleteValidator<ProjectFileDeleteCommand>
{
    public ProjectFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}