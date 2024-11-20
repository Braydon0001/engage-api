namespace Engage.Application.Services.ProjectTasks.Commands;

public class ProjectTaskFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record ProjectTaskFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProjectTaskFileDeleteCommand, bool>
{
    public async Task<bool> Handle(ProjectTaskFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectTasks.SingleOrDefaultAsync(e => e.ProjectTaskId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(ProjectTask), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class ProjectTaskFileDeleteValidator : FileDeleteValidator<ProjectTaskFileDeleteCommand>
{
    public ProjectTaskFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}