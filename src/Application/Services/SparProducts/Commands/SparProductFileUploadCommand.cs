namespace Engage.Application.Services.SparProducts.Commands;

public class SparProductFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record SparProductFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<SparProductFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(SparProductFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SparProducts.SingleOrDefaultAsync(e => e.SparProductId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(SparProduct),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class SparProductFileUploadValidator : FileUploadValidator<SparProductFileUploadCommand>
{
    public SparProductFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}