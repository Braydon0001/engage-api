namespace Engage.Application.Services.SupplierAllowanceSubContracts.Commands;

public class SupplierAllowanceSubContractUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class SupplierAllowanceSubContractUploadFileHandler : FileUploadHandler, IRequestHandler<SupplierAllowanceSubContractUploadFileCommand, OperationStatus>
{
    public SupplierAllowanceSubContractUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(SupplierAllowanceSubContractUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierAllowanceSubContracts.SingleOrDefaultAsync(e => e.SupplierAllowanceSubContractId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(SupplierAllowanceSubContract),
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

public class SupplierAllowanceSubContractUploadFileValidator : FileUploadValidator<SupplierAllowanceSubContractUploadFileCommand>
{
    public SupplierAllowanceSubContractUploadFileValidator()
    {
    }
}