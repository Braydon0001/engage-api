namespace Engage.Application.Services.EmployeeSuspensions.Commands;

public class EmployeeSuspensionDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class EmployeeSuspensionDeleteFileHandler : FileDeleteHandler, IRequestHandler<EmployeeSuspensionDeleteFileCommand, OperationStatus>
{
    public EmployeeSuspensionDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeSuspensionDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeSuspensions.SingleOrDefaultAsync(e => e.EmployeeSuspensionId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(EmployeeSuspension), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class EmployeeSuspensionDeleteFileValidator : FileDeleteValidator<EmployeeSuspensionDeleteFileCommand>
{
    public EmployeeSuspensionDeleteFileValidator()
    {
    }
}