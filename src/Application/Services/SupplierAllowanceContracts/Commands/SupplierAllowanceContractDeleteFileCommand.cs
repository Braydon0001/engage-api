namespace Engage.Application.Services.SupplierAllowanceContracts.Commands;

public class SupplierAllowanceContractDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class SupplierAllowanceContractDeleteFileHandler : FileDeleteHandler, IRequestHandler<SupplierAllowanceContractDeleteFileCommand, OperationStatus>
{
    public SupplierAllowanceContractDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(SupplierAllowanceContractDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierAllowanceContracts.SingleOrDefaultAsync(e => e.SupplierAllowanceContractId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(SupplierAllowanceContract), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class SupplierAllowanceContractDeleteFileValidator : FileDeleteValidator<SupplierAllowanceContractDeleteFileCommand>
{
    public SupplierAllowanceContractDeleteFileValidator()
    {
    }
}