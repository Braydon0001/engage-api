namespace Engage.Application.Services.Suppliers.Commands;

public class SupplierFileUploadCommand : FileUploadCommand, IRequest<JsonFile>
{
}

public class SupplierFileUploadHandler : FileUploadHandler, IRequestHandler<SupplierFileUploadCommand, JsonFile>
{
    public SupplierFileUploadHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<JsonFile> Handle(SupplierFileUploadCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Suppliers.SingleOrDefaultAsync(e => e.SupplierId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(Supplier),
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

public class SupplierFileUploadValidator : FileUploadValidator<SupplierFileUploadCommand>
{
    public SupplierFileUploadValidator()
    {
    }
}
