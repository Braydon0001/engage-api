namespace Engage.Application.Services.EngageMasterProducts.Commands;

public class EngageMasterProductUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class EngageMasterProductUploadFileHandler : FileUploadHandler, IRequestHandler<EngageMasterProductUploadFileCommand, OperationStatus>
{
    public EngageMasterProductUploadFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(EngageMasterProductUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageMasterProducts.SingleOrDefaultAsync(e => e.EngageMasterProductId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(EngageMasterProduct),
            EntityFiles = entity.Files,
            MaxFiles = 5,
            OverwriteType = false
        };

        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        operationStatus.ReturnObject = file;
        return operationStatus;
    }
}

public class EngageMasterProductUploadFileValidator : FileUploadValidator<EngageMasterProductUploadFileCommand>
{
    public EngageMasterProductUploadFileValidator()
    {
    }
}