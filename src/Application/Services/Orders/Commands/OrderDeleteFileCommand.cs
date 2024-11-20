namespace Engage.Application.Services.Orders.Commands;

public class OrderDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class OrderDeleteFileHandler : FileDeleteHandler, IRequestHandler<OrderDeleteFileCommand, OperationStatus>
{
    public OrderDeleteFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(OrderDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders.SingleOrDefaultAsync(e => e.OrderId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(Order), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class OrderDeleteFileValidator : FileDeleteValidator<OrderDeleteFileCommand>
{
    public OrderDeleteFileValidator()
    {
    }
}