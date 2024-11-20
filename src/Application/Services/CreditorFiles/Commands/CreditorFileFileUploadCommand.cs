namespace Engage.Application.Services.CreditorFiles.Commands;

public class CreditorFileFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record CreditorFileFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<CreditorFileFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(CreditorFileFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CreditorFiles.SingleOrDefaultAsync(e => e.CreditorFileId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(CreditorFile),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class CreditorFileFileUploadValidator : FileUploadValidator<CreditorFileFileUploadCommand>
{
    public CreditorFileFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}