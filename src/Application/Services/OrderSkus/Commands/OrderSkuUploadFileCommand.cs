namespace Engage.Application.Services.OrderSkus.Commands;

public class OrderSkuUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class OrderSkuUploadFileHandler : FileUploadHandler, IRequestHandler<OrderSkuUploadFileCommand, OperationStatus>
{
    public OrderSkuUploadFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(OrderSkuUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderSkus.SingleOrDefaultAsync(e => e.OrderSkuId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(OrderSku),
            EntityFiles = entity.Files,
            MaxFiles = 1,
        };

        var file = await _file.UploadAsync(command, options, cancellationToken);

        entity.Files = entity.Files.AddFile(file);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;
        operationStatus.ReturnObject = file;
        return operationStatus;
    }
}

public class OrderSkuUploadFileValidator : FileUploadValidator<OrderSkuUploadFileCommand>
{
    public OrderSkuUploadFileValidator()
    {
    }
}