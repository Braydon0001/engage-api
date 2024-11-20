namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class OrderSkuDeleteFileHandler : FileDeleteHandler, IRequestHandler<OrderSkuDeleteFileCommand, OperationStatus>
{
    public OrderSkuDeleteFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(OrderSkuDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderSkus.SingleOrDefaultAsync(e => e.OrderSkuId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(OrderSku), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class OrderSkuDeleteFileValidator : FileDeleteValidator<OrderSkuDeleteFileCommand>
{
    public OrderSkuDeleteFileValidator()
    {
    }
}