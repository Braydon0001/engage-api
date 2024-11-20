namespace Engage.Application.Services.EmployeeSuspensions.Commands;

public class EmployeeSuspensionUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class EmployeeSuspensionUploadFileHandler : FileUploadHandler, IRequestHandler<EmployeeSuspensionUploadFileCommand, OperationStatus>
{
    public EmployeeSuspensionUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeSuspensionUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeSuspensions.SingleOrDefaultAsync(e => e.EmployeeSuspensionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(EmployeeSuspension),
            EntityFiles = entity.Files,
            MaxFiles = 3
        };

        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        operationStatus.ReturnObject = file;
        return operationStatus;
    }
}

public class EmployeeSuspensionUploadFileValidator : FileUploadValidator<EmployeeSuspensionUploadFileCommand>
{
    public EmployeeSuspensionUploadFileValidator()
    {
    }
}