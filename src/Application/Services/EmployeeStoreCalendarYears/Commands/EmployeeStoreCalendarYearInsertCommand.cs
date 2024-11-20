// auto-generated
namespace Engage.Application.Services.EmployeeStoreCalendarYears.Commands;

public class EmployeeStoreCalendarYearInsertCommand : IMapTo<EmployeeStoreCalendarYear>, IRequest<EmployeeStoreCalendarYear>
{
    public string Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStoreCalendarYearInsertCommand, EmployeeStoreCalendarYear>();
    }
}

public class EmployeeStoreCalendarYearInsertHandler : InsertHandler, IRequestHandler<EmployeeStoreCalendarYearInsertCommand, EmployeeStoreCalendarYear>
{
    public EmployeeStoreCalendarYearInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarYear> Handle(EmployeeStoreCalendarYearInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeStoreCalendarYearInsertCommand, EmployeeStoreCalendarYear>(command);

        _context.EmployeeStoreCalendarYears.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeStoreCalendarYearInsertValidator : AbstractValidator<EmployeeStoreCalendarYearInsertCommand>
{
    public EmployeeStoreCalendarYearInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate).NotEmpty();
        RuleFor(e => e.EndDate).NotEmpty();
    }
}