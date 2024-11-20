namespace Engage.Application.Services.SupplierAllowances.Commands;

public class SupplierAllowanceDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class SupplierAllowanceDeleteFileHandler : FileDeleteHandler, IRequestHandler<SupplierAllowanceDeleteFileCommand, OperationStatus>
{
    public SupplierAllowanceDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(SupplierAllowanceDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.SupplierAllowances.SingleOrDefaultAsync(e => e.SupplierAllowanceId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(SupplierAllowance), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class SupplierAllowanceDeleteFileValidator : FileDeleteValidator<SupplierAllowanceDeleteFileCommand>
{
    public SupplierAllowanceDeleteFileValidator()
    {
    }
}