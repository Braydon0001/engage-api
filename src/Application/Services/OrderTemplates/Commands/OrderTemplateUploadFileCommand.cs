namespace Engage.Application.Services.OrderTemplates.Commands;

public class OrderTemplateUploadFileCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class OrderTemplateUploadFileHandler : FileUploadHandler, IRequestHandler<OrderTemplateUploadFileCommand, OperationStatus>
{
    public OrderTemplateUploadFileHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(OrderTemplateUploadFileCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderTemplates.SingleOrDefaultAsync(e => e.OrderTemplateId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(OrderTemplate),
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

public class OrderTemplateUploadFileValidator : FileUploadValidator<OrderTemplateUploadFileCommand>
{
    public OrderTemplateUploadFileValidator()
    {
    }
}