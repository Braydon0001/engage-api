namespace Engage.Application.Services.EngageVariantProducts.Commands;

public class EngageVariantProductUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class EngageVariantProductUploadFileHandler : FileUploadHandler, IRequestHandler<EngageVariantProductUploadFileCommand, OperationStatus>
{
    public EngageVariantProductUploadFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(EngageVariantProductUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageVariantProducts.SingleOrDefaultAsync(e => e.EngageVariantProductId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(EngageVariantProduct),
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

public class EngageVariantProductUploadFileValidator : FileUploadValidator<EngageVariantProductUploadFileCommand>
{
    public EngageVariantProductUploadFileValidator()
    {
    }
}