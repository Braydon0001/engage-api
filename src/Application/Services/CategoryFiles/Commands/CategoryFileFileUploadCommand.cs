namespace Engage.Application.Services.CategoryFiles.Commands;

public class CategoryFileFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record CategoryFileFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<CategoryFileFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(CategoryFileFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CategoryFiles.SingleOrDefaultAsync(e => e.CategoryFileId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(CategoryFile),
            EntityFiles = entity.Files,
            MaxFiles = 1,
            SetHeaders = true
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class CategoryFileFileUploadValidator : FileUploadValidator<CategoryFileFileUploadCommand>
{
    public CategoryFileFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}