namespace Engage.Application.Services.EmployeeQualifications.Commands;

public class EmployeeQualificationUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class EmployeeQualificationUploadFileHandler : FileUploadHandler, IRequestHandler<EmployeeQualificationUploadFileCommand, OperationStatus>
{
    public EmployeeQualificationUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeQualificationUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeQualifications.SingleOrDefaultAsync(e => e.EmployeeQualificationId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(EmployeeQualification),
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

public class EmployeeQualificationUploadFileValidator : FileUploadValidator<EmployeeQualificationUploadFileCommand>
{
    public EmployeeQualificationUploadFileValidator()
    {
    }
}