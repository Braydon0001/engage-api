namespace Engage.Application.Services.SupplierAllowanceSubContracts.Commands;

public class SupplierAllowanceSubContractDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class SupplierAllowanceSubContractDeleteFileHandler : FileDeleteHandler, IRequestHandler<SupplierAllowanceSubContractDeleteFileCommand, OperationStatus>
{
    public SupplierAllowanceSubContractDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(SupplierAllowanceSubContractDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierAllowanceSubContracts.SingleOrDefaultAsync(e => e.SupplierAllowanceSubContractId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(SupplierAllowanceSubContract), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class SupplierAllowanceSubContractDeleteFileValidator : FileDeleteValidator<SupplierAllowanceSubContractDeleteFileCommand>
{
    public SupplierAllowanceSubContractDeleteFileValidator()
    {
    }
}