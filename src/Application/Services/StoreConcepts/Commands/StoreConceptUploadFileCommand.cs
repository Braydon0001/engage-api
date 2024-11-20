namespace Engage.Application.Services.StoreConcepts.Commands;

public class StoreConceptUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class StoreConceptUploadFileHandler : FileUploadHandler, IRequestHandler<StoreConceptUploadFileCommand, OperationStatus>
{
    public StoreConceptUploadFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreConcepts.SingleOrDefaultAsync(e => e.Id == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(StoreConcept),
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

public class StoreConceptUploadFileValidator : FileUploadValidator<StoreConceptUploadFileCommand>
{
    public StoreConceptUploadFileValidator()
    {
    }
}