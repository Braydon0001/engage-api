// auto-generated
namespace Engage.Application.Services.InventoryTransactions.Commands;

public class InventoryTransactionFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public class InventoryTransactionFileUploadHandler : FileUploadHandler, IRequestHandler<InventoryTransactionFileUploadCommand, JsonFile>
{
    public InventoryTransactionFileUploadHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<JsonFile> Handle(InventoryTransactionFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.InventoryTransactions.SingleOrDefaultAsync(e => e.InventoryTransactionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(InventoryTransaction),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };
        var file = await _file.UploadAsync(command, options, cancellationToken);

         entity.Files = entity.Files.AddFile(file);

        await _context.SaveChangesAsync(cancellationToken);

        return file;
    }
}

public class InventoryTransactionFileUploadValidator : FileUploadValidator<InventoryTransactionFileUploadCommand>
{
    public InventoryTransactionFileUploadValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.File).NotNull();
    }
}