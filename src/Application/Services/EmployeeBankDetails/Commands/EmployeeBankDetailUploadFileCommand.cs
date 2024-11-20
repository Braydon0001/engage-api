namespace Engage.Application.Services.EmployeeBankDetails.Commands;

public class EmployeeBankDetailUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class EmployeeBankDetailUploadFileHandler : FileUploadHandler, IRequestHandler<EmployeeBankDetailUploadFileCommand, OperationStatus>
{
    public EmployeeBankDetailUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeBankDetailUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeBankDetails.SingleOrDefaultAsync(e => e.EmployeeBankDetailId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(EmployeeBankDetail),
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

public class EmployeeBankDetailUploadFileValidator : FileUploadValidator<EmployeeBankDetailUploadFileCommand>
{
    public EmployeeBankDetailUploadFileValidator()
    {
    }
}