namespace Engage.Application.Services.EmployeeQualifications.Commands;

public class EmployeeQualificationDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class EmployeeQualificationDeleteFileHandler : FileDeleteHandler, IRequestHandler<EmployeeQualificationDeleteFileCommand, OperationStatus>
{
    public EmployeeQualificationDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeQualificationDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeQualifications.SingleOrDefaultAsync(e => e.EmployeeQualificationId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(EmployeeQualification), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class EmployeeQualificationDeleteFileValidator : FileDeleteValidator<EmployeeQualificationDeleteFileCommand>
{
    public EmployeeQualificationDeleteFileValidator()
    {
    }
}