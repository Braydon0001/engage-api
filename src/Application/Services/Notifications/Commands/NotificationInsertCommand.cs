namespace Engage.Application.Services.Notifications.Commands;

public class NotificationInsertCommand : NotificationCommand, IRequest<OperationStatus>
{
}

public class NotificationInsertHandler : BaseCreateCommandHandler, IRequestHandler<NotificationInsertCommand, OperationStatus>
{
    public NotificationInsertHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(NotificationInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<NotificationInsertCommand, Notification>(command);
        _context.Notifications.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status == true)
        {
            opStatus.OperationId = entity.NotificationId;
            await NotificationAssigns.BatchAssign(_mediator, command, entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return opStatus;
    }
}
