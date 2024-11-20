namespace Engage.Application.Services.EmployeeAssets.Commands;

public class EmployeeAssetUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class EmployeeUploadFileHandler : FileUploadHandler, IRequestHandler<EmployeeAssetUploadFileCommand, OperationStatus>
{
    public EmployeeUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeAssetUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeAssets.SingleOrDefaultAsync(e => e.EmployeeAssetId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(EmployeeAsset),
            EntityFiles = entity.Files,
            MaxFiles = 6,
        };

        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        operationStatus.ReturnObject = file;
        return operationStatus;
    }
}

public class EmployeeUploadFileValidator : FileUploadValidator<EmployeeAssetUploadFileCommand>
{
    public EmployeeUploadFileValidator()
    {
    }
}