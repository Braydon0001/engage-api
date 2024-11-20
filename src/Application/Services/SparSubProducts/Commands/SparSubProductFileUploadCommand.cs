namespace Engage.Application.Services.SparSubProducts.Commands;

public class SparSubProductFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record SparSubProductFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SparSubProductFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(SparSubProductFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SparSubProducts.SingleOrDefaultAsync(e => e.SparSubProductId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(SparSubProduct),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class SparSubProductFileUploadValidator : FileUploadValidator<SparSubProductFileUploadCommand>
{
    public SparSubProductFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}