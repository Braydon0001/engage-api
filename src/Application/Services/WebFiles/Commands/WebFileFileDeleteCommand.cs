// auto-generated
namespace Engage.Application.Services.WebFiles.Commands;

public class WebFileFileDeleteCommand : FileDeleteCommand, IRequest<bool>
{
}

public class WebFileFileDeleteHandler : FileDeleteHandler, IRequestHandler<WebFileFileDeleteCommand, bool>
{
    public WebFileFileDeleteHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<bool> Handle(WebFileFileDeleteCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return false;
        }

        await _file.DeleteAsync(command, nameof(WebFile), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);
        await _context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}

public class WebFileFileDeleteValidator : FileDeleteValidator<WebFileFileDeleteCommand>
{
    public WebFileFileDeleteValidator()
    {
    }
}