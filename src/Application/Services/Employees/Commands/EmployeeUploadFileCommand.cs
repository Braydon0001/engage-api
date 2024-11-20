namespace Engage.Application.Services.Employees.Commands;

public class EmployeeUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class EmployeeUploadFileHandler : FileUploadHandler, IRequestHandler<EmployeeUploadFileCommand, OperationStatus>
{
    public EmployeeUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.SingleOrDefaultAsync(e => e.EmployeeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(Employee),
            EntityFiles = entity.Files,
            MaxFiles = 14,
        };

        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        operationStatus.ReturnObject = file;
        return operationStatus;
    }
}

public class EmployeeUploadFileValidator : FileUploadValidator<EmployeeUploadFileCommand>
{
    public EmployeeUploadFileValidator()
    {
    }
}