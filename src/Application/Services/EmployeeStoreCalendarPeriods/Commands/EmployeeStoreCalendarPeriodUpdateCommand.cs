// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarPeriods.Commands;

public class EmployeeStoreCalendarPeriodUpdateCommand : IMapTo<EmployeeStoreCalendarPeriod>, IRequest<EmployeeStoreCalendarPeriod>
{
    public int Id { get; set; }
    public int EmployeeStoreCalendarYearId { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarPeriodUpdateCommand, EmployeeStoreCalendarPeriod>();
    }
}

public class EmployeeStoreCalendarPeriodUpdateHandler : UpdateHandler, IRequestHandler<EmployeeStoreCalendarPeriodUpdateCommand, EmployeeStoreCalendarPeriod>
{
    public EmployeeStoreCalendarPeriodUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarPeriod> Handle(EmployeeStoreCalendarPeriodUpdateCommand command, CancellationToken cancellationToken)
    {
        if (command.EndDate < command.StartDate)
        {
            throw new Exception("Start date must be before end date");
        }

        var entity = await _context.EmployeeStoreCalendarPeriods.SingleOrDefaultAsync(e => e.EmployeeStoreCalendarPeriodId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var previousPeriods = await _context.EmployeeStoreCalendarPeriods
                            .Where(e =>
                            command.StartDate >= e.StartDate && command.StartDate <= e.EndDate
                                && e.EmployeeStoreCalendarPeriodId != entity.EmployeeStoreCalendarPeriodId
                            || command.EndDate >= e.StartDate && command.EndDate <= e.EndDate
                                && e.EmployeeStoreCalendarPeriodId != entity.EmployeeStoreCalendarPeriodId
                            )
                            .FirstOrDefaultAsync(cancellationToken);

        if (previousPeriods != null)
        {
            throw new Exception("Period cannot intercept a different period");
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeStoreCalendarPeriodValidator : AbstractValidator<EmployeeStoreCalendarPeriodUpdateCommand>
{
    public UpdateEmployeeStoreCalendarPeriodValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeStoreCalendarYearId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Number).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}