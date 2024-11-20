namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarVisitQuery : IRequest<EmployeeStoreCalendarDto>
{
    public int Id { get; set; }
}
public class EmployeeStoreCalendarVisitHandler : BaseQueryHandler, IRequestHandler<EmployeeStoreCalendarVisitQuery, EmployeeStoreCalendarDto>
{
    public EmployeeStoreCalendarVisitHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeStoreCalendarDto> Handle(EmployeeStoreCalendarVisitQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeStoreCalendars
            .AsNoTracking()
            .ProjectTo<EmployeeStoreCalendarDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        return entity;
    }
}