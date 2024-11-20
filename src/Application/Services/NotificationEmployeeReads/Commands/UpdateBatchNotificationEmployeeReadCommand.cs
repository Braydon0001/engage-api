namespace Engage.Application.Services.NotificationEmployeeReads.Commands;

//Command
public class UpdateBatchNotificationEmployeeReadCommand : IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
    public List<int> NotificationIds { get; set; }
}

//Handler
public class UpdateBatchNotificationEmployeeReadCommandHandler : BaseCreateCommandHandler, IRequestHandler<UpdateBatchNotificationEmployeeReadCommand, OperationStatus>
{
    private readonly IDateTimeService _date;
    public UpdateBatchNotificationEmployeeReadCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IDateTimeService date) : base(context, mapper, mediator)
    {
        _date = date;
    }

    public async Task<OperationStatus> Handle(UpdateBatchNotificationEmployeeReadCommand command, CancellationToken cancellationToken)
    {
        foreach (var notificationId in command.NotificationIds)
        {
            var updateReadCommand = new UpdateNotificationEmployeeReadCommand
            {
                EmployeeId = command.EmployeeId,
                NotificationId = notificationId,
                SaveChanges = false,
            };
            await _mediator.Send(updateReadCommand, cancellationToken);
        }
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
