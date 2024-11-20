namespace Engage.Application.Services.NotificationEmployeeReads.Commands;

public class UpdateNotificationEmployeeReadCommand : IRequest<OperationStatus>
{
    public int NotificationId { get; set; }
    public int EmployeeId { get; set; }
    public bool SaveChanges { get; set; } = true;
}


//Handlers

public class UpdateNotificationEmployeeReadCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateNotificationEmployeeReadCommand, OperationStatus>
{
    private readonly IDateTimeService _date;

    public UpdateNotificationEmployeeReadCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IDateTimeService date) : base(context, mapper, mediator)
    {
        _date = date;
    }

    public async Task<OperationStatus> Handle(UpdateNotificationEmployeeReadCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.NotificationEmployeeReads.SingleOrDefaultAsync(x => x.NotificationId == command.NotificationId && x.EmployeeId == command.EmployeeId, cancellationToken);
        if (entity == null)
        {
            var newEntity = new NotificationEmployeeRead
            {
                NotificationId = command.NotificationId,
                EmployeeId = command.EmployeeId,
                ReadDate = _date.Now
            };
            _context.NotificationEmployeeReads.Add(newEntity);
        }
        else
        {
            entity.ReadDate = _date.Now;
        }


        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = command.EmployeeId;

            return opStatus;
        }
        return new OperationStatus(status: true);
    }
}
