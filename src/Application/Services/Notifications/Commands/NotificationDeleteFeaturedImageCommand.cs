namespace Engage.Application.Services.Notifications.Commands;

public class NotificationDeleteFeaturedImageCommand : FileDeleteCommand, IRequest<OperationStatus>
{
}

public class NotificationDeleteFeaturedImageCommandHandler : FileDeleteHandler, IRequestHandler<NotificationDeleteFeaturedImageCommand, OperationStatus>
{
    public NotificationDeleteFeaturedImageCommandHandler(IAppDbContext context, IFileService file) : base(context, file)
    {
    }

    public async Task<OperationStatus> Handle(NotificationDeleteFeaturedImageCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleOrDefaultAsync(e => e.NotificationId == command.Id, cancellationToken);
        if (entity == null || entity.Files == null || !entity.Files.FileExists(command))
        {
            return null;
        }

        await _file.DeleteAsync(command, nameof(Notification), cancellationToken);

        entity.Files = entity.Files.RemoveFile(command);

        var operationStatus = await _context.SaveChangesAsync(cancellationToken);
        operationStatus.OperationId = command.Id;

        return operationStatus;
    }
}

public class NotificationDeleteFeaturedImageValidator : FileDeleteValidator<NotificationDeleteFeaturedImageCommand>
{
    public NotificationDeleteFeaturedImageValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
    }
}
