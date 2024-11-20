namespace Engage.Application.Services.Inventories.Commands;

public class InventoryFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public record InventoryFileUploadHandler(IAppDbContext Context, IFileService File) : IRequestHandler<InventoryFileUploadCommand, JsonFile>
{
    public async Task<JsonFile> Handle(InventoryFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Inventories.SingleOrDefaultAsync(e => e.InventoryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(Inventory),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await File.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        await Context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class InventoryFileUploadValidator : FileUploadValidator<InventoryFileUploadCommand>
{
    public InventoryFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}