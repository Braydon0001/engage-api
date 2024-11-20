namespace Engage.Application.Services.CommunicationHistoryEmployeeStoreCalendars.Queries;

public class CommunicationHistoryEmployeeStoreCalendarListQuery : IRequest<List<CommunicationHistoryEmployeeStoreCalendarDto>>
{
    public int? EmployeeStoreCalendarId { get; set; }
}

public record CommunicationHistoryEmployeeStoreCalendarListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationHistoryEmployeeStoreCalendarListQuery, List<CommunicationHistoryEmployeeStoreCalendarDto>>
{
    public async Task<List<CommunicationHistoryEmployeeStoreCalendarDto>> Handle(CommunicationHistoryEmployeeStoreCalendarListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationHistoryEmployeeStoreCalendars.AsQueryable().AsNoTracking();

        if (query.EmployeeStoreCalendarId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeStoreCalendarId == query.EmployeeStoreCalendarId.Value);
        }

        return await queryable.OrderBy(e => e.CommunicationHistoryId)
                              .ProjectTo<CommunicationHistoryEmployeeStoreCalendarDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}