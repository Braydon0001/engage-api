namespace Engage.Application.Services.SupplierAllowances.Commands;

public class SupplierAllowanceUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class SupplierAllowanceUploadFileHandler : FileUploadHandler, IRequestHandler<SupplierAllowanceUploadFileCommand, OperationStatus>
{
    public SupplierAllowanceUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(SupplierAllowanceUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierAllowances.SingleOrDefaultAsync(e => e.SupplierAllowanceId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(SupplierAllowance),
            EntityFiles = entity.Files,
            MaxFiles = 6,
        };

        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        operationStatus.ReturnObject = file;
        return operationStatus;
    }
}

public class SupplierAllowanceUploadFileValidator : FileUploadValidator<SupplierAllowanceUploadFileCommand>
{
    public SupplierAllowanceUploadFileValidator()
    {
    }
}