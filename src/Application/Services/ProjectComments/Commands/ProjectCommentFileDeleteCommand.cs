namespace Engage.Application.Services.ProjectComments.Commands;

public class ProjectCommentFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record ProjectCommentFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<ProjectCommentFileDeleteCommand, bool>
{
    public async Task<bool> Handle(ProjectCommentFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectComments.SingleOrDefaultAsync(e => e.ProjectCommentId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(ProjectComment), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class ProjectCommentFileDeleteValidator : FileDeleteValidator<ProjectCommentFileDeleteCommand>
{
    public ProjectCommentFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}