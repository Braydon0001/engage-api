namespace Engage.Application.Services.OrderTemplateProducts.Commands;

public class OrderTemplateProductDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class OrderTemplateProductDeleteFileHandler : FileDeleteHandler, IRequestHandler<OrderTemplateProductDeleteFileCommand, OperationStatus>
{
    public OrderTemplateProductDeleteFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(OrderTemplateProductDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplateProducts.SingleOrDefaultAsync(e => e.OrderTemplateProductId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(OrderTemplateProduct), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class OrderTemplateProductDeleteFileValidator : FileDeleteValidator<OrderTemplateProductDeleteFileCommand>
{
    public OrderTemplateProductDeleteFileValidator()
    {
    }
}