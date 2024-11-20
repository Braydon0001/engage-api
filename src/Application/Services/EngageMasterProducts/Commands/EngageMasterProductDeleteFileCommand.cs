namespace Engage.Application.Services.EngageMasterProducts.Commands;

public class EngageMasterProductDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class EngageMasterProductDeleteFileHandler : FileDeleteHandler, IRequestHandler<EngageMasterProductDeleteFileCommand, OperationStatus>
{
    public EngageMasterProductDeleteFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(EngageMasterProductDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageMasterProducts.SingleOrDefaultAsync(e => e.EngageMasterProductId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(EngageMasterProduct), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class EngageMasterProductDeleteFileValidator : FileDeleteValidator<EngageMasterProductDeleteFileCommand>
{
    public EngageMasterProductDeleteFileValidator()
    {
    }
}