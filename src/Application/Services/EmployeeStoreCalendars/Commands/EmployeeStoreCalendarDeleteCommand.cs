namespace Engage.Application.Services.EmployeeStoreCalendars.Commands;

public class EmployeeStoreCalendarDeleteCommand : IRequest<OperationStatus>
{
    public int EmployeeStoreCalendarId { get; set; }
}
public class EmployeeStoreCalendarDeleteHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeStoreCalendarDeleteCommand, OperationStatus>
{
    public EmployeeStoreCalendarDeleteHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeStoreCalendarDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStoreCalendars
                                    .Where(e => e.EmployeeStoreCalendarId == request.EmployeeStoreCalendarId)
                                    .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new Exception("Store Appointment not found");
        }
        entity.Disabled = true;
        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}