// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarBlockDays.Commands;

public class EmployeeStoreCalendarBlockDayUpdateCommand : IMapTo<EmployeeStoreCalendarBlockDay>, IRequest<EmployeeStoreCalendarBlockDay>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeeStoreCalendarTypeId { get; set; }
    public int EmployeeStoreCalendarStatusId { get; set; }
    public DateTime CalendarDate { get; set; }
    public bool IsManagerCreated { get; set; }
    public int EmployeeStoreCalendarPeriodId { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarBlockDayUpdateCommand, EmployeeStoreCalendarBlockDay>();
    }
}

public class EmployeeStoreCalendarBlockDayUpdateHandler : UpdateHandler, IRequestHandler<EmployeeStoreCalendarBlockDayUpdateCommand, EmployeeStoreCalendarBlockDay>
{
    public EmployeeStoreCalendarBlockDayUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarBlockDay> Handle(EmployeeStoreCalendarBlockDayUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStoreCalendarBlockDays.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarBlockDayId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var calendarEntries = await _context.EmployeeStoreCalendars
                                            .Where(e => e.CalendarDate.Date == command.CalendarDate.Date
                                                && e.EmployeeId == command.EmployeeId
                                                && e.Disabled == false)
                                            .FirstOrDefaultAsync(cancellationToken);

        if (entity.EmployeeStoreCalendarTypeId == (int)EmployeeStoreCalendarTypeId.BlockDay && calendarEntries != null)
        {
            throw new Exception("Cannot block day with store visits");
        }

        var blockedDay = await _context.EmployeeStoreCalendarBlockDays
                                       .Where(e => e.EmployeeId == command.EmployeeId
                                       && e.CalendarDate.Date == command.CalendarDate
                                       && e.Disabled == false
                                       && e.EmployeeStoreCalendarTypeId == (int)EmployeeStoreCalendarTypeId.BlockDay)
                                       .FirstOrDefaultAsync(cancellationToken);

        if (blockedDay != null)
        {
            throw new Exception("Day is already blocked out");
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeStoreCalendarBlockDayValidator : AbstractValidator<EmployeeStoreCalendarBlockDayUpdateCommand>
{
    public UpdateEmployeeStoreCalendarBlockDayValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeStoreCalendarTypeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeStoreCalendarStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CalendarDate).NotEmpty();
        RuleFor(e => e.IsManagerCreated);
        RuleFor(e => e.EmployeeStoreCalendarPeriodId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Note).NotEmpty().MaximumLength(100);
    }
}