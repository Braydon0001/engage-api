namespace Engage.Application.Services.EmployeeStoreCalendarBlockDays.Commands;

public class EmployeeStoreCalendarBlockDayDeleteCommand : IRequest<OperationStatus>
{
    public int EmployeeStoreCalendarBlockDayId { get; set; }
}
public class EmployeeStoreCalendarBlockDayDeleteHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeStoreCalendarBlockDayDeleteCommand, OperationStatus>
{
    public EmployeeStoreCalendarBlockDayDeleteHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeStoreCalendarBlockDayDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStoreCalendarBlockDays
                                   .Where(e => e.EmployeeStoreCalendarBlockDayId == request.EmployeeStoreCalendarBlockDayId)
                                   .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new Exception("Blocked day not found");
        }

        entity.Disabled = true;
        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}
