namespace Engage.Application.Services.StoreConcepts.Commands;

public class StoreConceptDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class StoreConceptDeleteFileHandler : FileDeleteHandler, IRequestHandler<StoreConceptDeleteFileCommand, OperationStatus>
{
    public StoreConceptDeleteFileHandler(IAppDbContext context, IFileService fileStorage) : base(context, fileStorage)
    {
    }

    public async Task<OperationStatus> Handle(StoreConceptDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.StoreConcepts.SingleOrDefaultAsync(e => e.Id == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(StoreConcept), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class StoreConceptDeleteFileValidator : FileDeleteValidator<StoreConceptDeleteFileCommand>
{
    public StoreConceptDeleteFileValidator()
    {
    }
}