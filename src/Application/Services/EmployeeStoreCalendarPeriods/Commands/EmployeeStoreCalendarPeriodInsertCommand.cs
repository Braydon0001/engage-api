// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarPeriods.Commands;

public class EmployeeStoreCalendarPeriodInsertCommand : IMapTo<EmployeeStoreCalendarPeriod>, IRequest<EmployeeStoreCalendarPeriod>
{
    public int EmployeeStoreCalendarYearId { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarPeriodInsertCommand, EmployeeStoreCalendarPeriod>();
    }
}

public class EmployeeStoreCalendarPeriodInsertHandler : InsertHandler, IRequestHandler<EmployeeStoreCalendarPeriodInsertCommand, EmployeeStoreCalendarPeriod>
{
    public EmployeeStoreCalendarPeriodInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarPeriod> Handle(EmployeeStoreCalendarPeriodInsertCommand command, CancellationToken cancellationToken)
    {
        if (command.EndDate < command.StartDate)
        {
            throw new Exception("Start date must be before end date");
        }

        var previousPeriods = await _context.EmployeeStoreCalendarPeriods
                            .Where(e =>
                            command.StartDate >= e.StartDate && command.StartDate <= e.EndDate
                            || command.EndDate >= e.StartDate && command.EndDate <= e.EndDate
                            )
                            .FirstOrDefaultAsync(cancellationToken);
        if (previousPeriods != null)
        {
            throw new Exception("Period cannot intercept a different period");
        }


        var entity = _mapper.Map<EmployeeStoreCalendarPeriodInsertCommand, EmployeeStoreCalendarPeriod>(command);

        _context.EmployeeStoreCalendarPeriods.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeStoreCalendarPeriodInsertValidator : AbstractValidator<EmployeeStoreCalendarPeriodInsertCommand>
{
    public EmployeeStoreCalendarPeriodInsertValidator()
    {
        RuleFor(e => e.EmployeeStoreCalendarYearId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Number).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}