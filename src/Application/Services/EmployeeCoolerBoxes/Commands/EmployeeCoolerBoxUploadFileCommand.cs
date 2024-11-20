namespace Engage.Application.Services.EmployeeCoolerBoxes.Commands;

public class EmployeeCoolerBoxUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class EmployeeCoolerBoxUploadFileHandler : FileUploadHandler, IRequestHandler<EmployeeCoolerBoxUploadFileCommand, OperationStatus>
{
    public EmployeeCoolerBoxUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeCoolerBoxUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeCoolerBoxes.SingleOrDefaultAsync(e => e.EmployeeCoolerBoxId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(EmployeeCoolerBox),
            EntityFiles = entity.Files,
            MaxFiles = 1
        };

        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        operationStatus.ReturnObject = file;
        return operationStatus;
    }
}

public class EmployeeCoolerBoxUploadFileValidator : FileUploadValidator<EmployeeCoolerBoxUploadFileCommand>
{
    public EmployeeCoolerBoxUploadFileValidator()
    {
    }
}