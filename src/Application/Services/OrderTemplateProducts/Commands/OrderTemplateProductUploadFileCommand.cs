namespace Engage.Application.Services.OrderTemplateProducts.Commands;

public class OrderTemplateProductUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class OrderTemplateProductUploadFileHandler : FileUploadHandler, IRequestHandler<OrderTemplateProductUploadFileCommand, OperationStatus>
{
    public OrderTemplateProductUploadFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(OrderTemplateProductUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplateProducts.SingleOrDefaultAsync(e => e.OrderTemplateProductId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(OrderTemplateProduct),
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

public class OrderTemplateProductUploadFileValidator : FileUploadValidator<OrderTemplateProductUploadFileCommand>
{
    public OrderTemplateProductUploadFileValidator()
    {
    }
}