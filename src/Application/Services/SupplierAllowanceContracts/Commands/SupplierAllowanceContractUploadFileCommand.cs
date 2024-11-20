namespace Engage.Application.Services.SupplierAllowanceContracts.Commands;

public class SupplierAllowanceContractUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class SupplierAllowanceContractUploadFileHandler : FileUploadHandler, IRequestHandler<SupplierAllowanceContractUploadFileCommand, OperationStatus>
{
    public SupplierAllowanceContractUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(SupplierAllowanceContractUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierAllowanceContracts.SingleOrDefaultAsync(e => e.SupplierAllowanceContractId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(SupplierAllowanceContract),
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

public class SupplierAllowanceContractUploadFileValidator : FileUploadValidator<SupplierAllowanceContractUploadFileCommand>
{
    public SupplierAllowanceContractUploadFileValidator()
    {
    }
}