namespace Engage.Application.Services.Notifications.Commands;

public class NotificationUploadFeaturedImageCommand : FileUploadCommand, IRequest<OperationStatus>
{
}

public class NotificationUploadFeaturedImageCommandHandler : FileUploadHandler, IRequestHandler<NotificationUploadFeaturedImageCommand, OperationStatus>
{
    public NotificationUploadFeaturedImageCommandHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(NotificationUploadFeaturedImageCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleOrDefaultAsync(e => e.NotificationId == command.Id);
        if (entity == null)
        {
            return null;
        }

        var options = new FileUploadOptions
        {
            ContainerName = nameof(Notification),
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

public class NotificationUploadFeaturedImageValidator : FileUploadValidator<NotificationUploadFeaturedImageCommand>
{
    public NotificationUploadFeaturedImageValidator()
    {
    }
}
