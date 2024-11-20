namespace Engage.Application.Services.EmployeeBankDetails.Commands;

public class EmployeeBankDetailDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class EmployeeBankDetailDeleteFileHandler : FileDeleteHandler, IRequestHandler<EmployeeBankDetailDeleteFileCommand, OperationStatus>
{
    public EmployeeBankDetailDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeBankDetailDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeBankDetails.SingleOrDefaultAsync(e => e.EmployeeBankDetailId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(EmployeeBankDetail), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class EmployeeBankDetailDeleteFileValidator : FileDeleteValidator<EmployeeBankDetailDeleteFileCommand>
{
    public EmployeeBankDetailDeleteFileValidator()
    {
    }
}