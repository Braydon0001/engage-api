// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarBlockDays.Commands;

public class EmployeeStoreCalendarBlockDayInsertCommand : IMapTo<EmployeeStoreCalendarBlockDay>, IRequest<EmployeeStoreCalendarBlockDay>
{
    public int EmployeeId { get; set; }
    public int EmployeeStoreCalendarTypeId { get; set; }
    //public int EmployeeStoreCalendarStatusId { get; set; }
    public DateTime CalendarDate { get; set; }
    public bool IsManagerCreated { get; set; }
    public string Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarBlockDayInsertCommand, EmployeeStoreCalendarBlockDay>();
    }
}

public class EmployeeStoreCalendarBlockDayInsertHandler : InsertHandler, IRequestHandler<EmployeeStoreCalendarBlockDayInsertCommand, EmployeeStoreCalendarBlockDay>
{
    public EmployeeStoreCalendarBlockDayInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarBlockDay> Handle(EmployeeStoreCalendarBlockDayInsertCommand command, CancellationToken cancellationToken)
    {
        if (command.EmployeeStoreCalendarTypeId == (int)EmployeeStoreCalendarTypeId.BlockDay)
        {
            //check if the selected day is already blocked out
            var pastBlockDay = await _context.EmployeeStoreCalendarBlockDays.SingleOrDefaultAsync(e => e.CalendarDate == command.CalendarDate
                                                    && e.EmployeeId == command.EmployeeId
                                                    && e.Disabled == false
                                                    && e.EmployeeStoreCalendarTypeId == (int)EmployeeStoreCalendarTypeId.BlockDay, cancellationToken);
            if (pastBlockDay != null)
            {
                throw new Exception("Day is already blocked out");
            }

            //Check if selected day already has store visits booked
            var storeVisits = await _context.EmployeeStoreCalendars.Where(e => e.CalendarDate == command.CalendarDate
                                                    && e.EmployeeId == command.EmployeeId && e.Disabled == false).FirstOrDefaultAsync(cancellationToken);
            if (storeVisits != null)
            {
                throw new Exception("This day already has store visits booked");
            }
            var externalSurveys = await _context.EmployeeStoreCalendarBlockDays.Where(e => e.CalendarDate == command.CalendarDate
                                                                                        && e.EmployeeId == command.EmployeeId && e.Disabled == false
                                                                                        && e.EmployeeStoreCalendarTypeId == (int)EmployeeStoreCalendarTypeId.Survey)
                                                                               .FirstOrDefaultAsync(cancellationToken);
            if (externalSurveys != null)
            {
                throw new Exception("This day already has surveys booked");
            }

            var disabledBlockDay = await _context.EmployeeStoreCalendarBlockDays.SingleOrDefaultAsync(e => e.CalendarDate == command.CalendarDate
                                                    && e.EmployeeId == command.EmployeeId
                                                    && e.Disabled == true, cancellationToken);

            //enable disabled block day
            if (disabledBlockDay != null)
            {
                disabledBlockDay.Disabled = false;
                disabledBlockDay.Note = command.Note;
                await _context.SaveChangesAsync(cancellationToken);
                return disabledBlockDay;
            }
        }

        //get calendar period
        var calendarPeriod = await _context.EmployeeStoreCalendarPeriods.SingleOrDefaultAsync(e =>
        command.CalendarDate.Date >= e.StartDate.Date && command.CalendarDate.Date <= e.EndDate.Date, cancellationToken);

        if (calendarPeriod == null)
        {
            throw new Exception("No Calendar Period Found");
        }

        var entity = _mapper.Map<EmployeeStoreCalendarBlockDayInsertCommand, EmployeeStoreCalendarBlockDay>(command);

        entity.EmployeeStoreCalendarPeriodId = calendarPeriod.EmployeeStoreCalendarPeriodId;

        //set type and status to default
        entity.EmployeeStoreCalendarStatusId = 1;
        //entity.EmployeeStoreCalendarTypeId = 1;

        _context.EmployeeStoreCalendarBlockDays.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeStoreCalendarBlockDayInsertValidator : AbstractValidator<EmployeeStoreCalendarBlockDayInsertCommand>
{
    public EmployeeStoreCalendarBlockDayInsertValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.EmployeeStoreCalendarTypeId).NotEmpty().GreaterThan(0);
        //RuleFor(e => e.EmployeeStoreCalendarStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CalendarDate).NotEmpty();
        RuleFor(e => e.IsManagerCreated);
        RuleFor(e => e.Note).NotEmpty().MaximumLength(100);
    }
}