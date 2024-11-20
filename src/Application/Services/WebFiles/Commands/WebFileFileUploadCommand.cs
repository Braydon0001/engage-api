// auto-generated

namespace Engage.Application.Services.WebFiles.Commands;

public class WebFileFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public class WebFileFileUploadHandler : FileUploadHandler, IRequestHandler<WebFileFileUploadCommand, JsonFile>
{
    public WebFileFileUploadHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<JsonFile> Handle(WebFileFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.WebFiles.SingleOrDefaultAsync(e => e.WebFileId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(WebFile),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await _file.UploadAsync(command, options, cancellationToken);

        var files = entity.Files ?? new List<JsonFile>();
        entity.Files = files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);
        
        return file;
    }
}

public class WebFileFileUploadValidator : FileUploadValidator<WebFileFileUploadCommand>
{
    public WebFileFileUploadValidator()
    {
    }
}