namespace Engage.Application.Services.Notifications.Commands;

public class NotificationUpdateCommand : NotificationCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class NotificationUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<NotificationUpdateCommand, OperationStatus>
{
    public NotificationUpdateHandler(IAppDbContext context, IMapper mapper, IMediator mediator) :
        base(context, mapper, mediator)
    { }

    public async Task<OperationStatus> Handle(NotificationUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Notifications.SingleAsync(x => x.NotificationId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        await NotificationAssigns.BatchAssign(_mediator, command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = command.Id;
        return opStatus;
    }
}
