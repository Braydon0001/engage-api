namespace Engage.Application.Services.EmployeeCoolerBoxes.Commands;

public class EmployeeCoolerBoxDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class EmployeeCoolerBoxDeleteFileHandler : FileDeleteHandler, IRequestHandler<EmployeeCoolerBoxDeleteFileCommand, OperationStatus>
{
    public EmployeeCoolerBoxDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeCoolerBoxDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeCoolerBoxes.SingleOrDefaultAsync(e => e.EmployeeCoolerBoxId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(EmployeeCoolerBox), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class EmployeeCoolerBoxDeleteFileValidator : FileDeleteValidator<EmployeeCoolerBoxDeleteFileCommand>
{
    public EmployeeCoolerBoxDeleteFileValidator()
    {
    }
}