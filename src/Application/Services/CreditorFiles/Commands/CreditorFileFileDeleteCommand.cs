namespace Engage.Application.Services.CreditorFiles.Commands;

public class CreditorFileFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public record CreditorFileFileDeleteHandler(IAppDbContext Context, IFileService File) : IRequestHandler<CreditorFileFileDeleteCommand, bool>
{
    public async Task<bool> Handle(CreditorFileFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CreditorFiles.SingleOrDefaultAsync(e => e.CreditorFileId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await File.DeleteAsync(command, nameof(CreditorFile), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        
        await Context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class CreditorFileFileDeleteValidator : FileDeleteValidator<CreditorFileFileDeleteCommand>
{
    public CreditorFileFileDeleteValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}