namespace Engage.Application.Services.EmployeeStoreCalendars.Queries;

public class EmployeeStoreCalendarCompletedListQuery : IRequest<ListResult<EmployeeStoreCalendarByEmployeeDto>>
{
    public int EmployeeId { get; set; }
}
public class EmployeeStoreCalendarCompletedListQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeStoreCalendarCompletedListQuery, ListResult<EmployeeStoreCalendarByEmployeeDto>>
{
    public EmployeeStoreCalendarCompletedListQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EmployeeStoreCalendarByEmployeeDto>> Handle(EmployeeStoreCalendarCompletedListQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeStoreCalendars.AsQueryable().Where(e => e.EmployeeId == request.EmployeeId && e.Disabled == false).AsNoTracking();

        var entities = await queryable
                                    .OrderByDescending(e => e.CalendarDate)
                                    .ThenBy(e => e.EmployeeStoreCalendarId)
                                    .ProjectTo<EmployeeStoreCalendarByEmployeeDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);

        return new ListResult<EmployeeStoreCalendarByEmployeeDto>
        {
            Count = entities.Count(),
            Data = entities
        };
    }
}
public class EmployeeStoreCalendarCompletedListQueryValidator : AbstractValidator<EmployeeStoreCalendarCompletedListQuery>
{
    public EmployeeStoreCalendarCompletedListQueryValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
    }
}