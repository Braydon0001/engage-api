namespace Engage.Application.Services.EngageVariantProducts.Commands;

public class EngageVariantProductDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class EngageVariantProductDeleteFileHandler : FileDeleteHandler, IRequestHandler<EngageVariantProductDeleteFileCommand, OperationStatus>
{
    public EngageVariantProductDeleteFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(EngageVariantProductDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageVariantProducts.SingleOrDefaultAsync(e => e.EngageVariantProductId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(EngageVariantProduct), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class EngageVariantProductDeleteFileValidator : FileDeleteValidator<EngageVariantProductDeleteFileCommand>
{
    public EngageVariantProductDeleteFileValidator()
    {
    }
}