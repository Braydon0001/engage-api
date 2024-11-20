namespace Engage.Application.Services.OrderTemplates.Commands;

public class OrderTemplateDeleteFileCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class OrderTemplateDeleteFileHandler : FileDeleteHandler, IRequestHandler<OrderTemplateDeleteFileCommand, OperationStatus>
{
    public OrderTemplateDeleteFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(OrderTemplateDeleteFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplates.SingleOrDefaultAsync(e => e.OrderTemplateId == command.Id, cancellationToken);
        if (entity == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(OrderTemplate), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        return operationStatus;
    }
}

public class OrderTemplateDeleteFileValidator : FileDeleteValidator<OrderTemplateDeleteFileCommand>
{
    public OrderTemplateDeleteFileValidator()
    {
    }
}