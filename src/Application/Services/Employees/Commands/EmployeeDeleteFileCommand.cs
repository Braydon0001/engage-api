namespace Engage.Application.Services.Employees.Commands;

public class EmployeeDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class EmployeeDeleteFileHandler : FileDeleteHandler, IRequestHandler<EmployeeDeleteFileCommand, OperationStatus>
{
    public EmployeeDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.SingleOrDefaultAsync(e => e.EmployeeId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(Employee), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class EmployeeDeleteFileValidator : FileDeleteValidator<EmployeeDeleteFileCommand>
{
    public EmployeeDeleteFileValidator()
    {
    }
}