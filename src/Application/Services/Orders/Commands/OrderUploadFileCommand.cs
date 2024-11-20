namespace Engage.Application.Services.Orders.Commands;

public class OrderUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class OrderUploadFileHandler : FileUploadHandler, IRequestHandler<OrderUploadFileCommand, OperationStatus>
{
    public OrderUploadFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(OrderUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders.SingleOrDefaultAsync(e => e.OrderId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(Order),
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

public class OrderUploadFileValidator : FileUploadValidator<OrderUploadFileCommand>
{
    public OrderUploadFileValidator()
    {
    }
}