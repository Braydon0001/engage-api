namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarGetByEmployeeQuery : GetQuery, IRequest<ListResult<EmployeeStoreCalendarDto>>
{
    public int EmployeeId { get; set; }
}
public class EmployeeStoreCalendarGetByEmployeeQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeStoreCalendarGetByEmployeeQuery, ListResult<EmployeeStoreCalendarDto>>
{
    public EmployeeStoreCalendarGetByEmployeeQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeStoreCalendarDto>> Handle(EmployeeStoreCalendarGetByEmployeeQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendars.AsQueryable().Where(e => e.EmployeeId == request.EmployeeId
                                                                                && e.Disabled == false).AsNoTracking();

        var entities = await queryable.OrderByDescending(e => e.CalendarDate)
                                    .ThenBy(e => e.EmployeeStoreCalendarId)
                                    .ProjectTo<EmployeeStoreCalendarDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);

        return new ListResult<EmployeeStoreCalendarDto>
        {
            Count = entities.Count(),
            Data = entities
        };
    }
}
public class EmployeeStoreCalendarGetByEmployeeQueryValidator : AbstractValidator<EmployeeStoreCalendarGetByEmployeeQuery>
{
    public EmployeeStoreCalendarGetByEmployeeQueryValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
    }
}
