namespace Engage.Application.Services.CommunicationHistoryEmployees.Queries;

public class CommunicationHistoryEmployeeListQuery : IRequest<List<CommunicationHistoryEmployeeDto>>
{
    public int? EmployeeId { get; set; }
}

public record CommunicationHistoryEmployeeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationHistoryEmployeeListQuery, List<CommunicationHistoryEmployeeDto>>
{
    public async Task<List<CommunicationHistoryEmployeeDto>> Handle(CommunicationHistoryEmployeeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationHistoryEmployees.AsQueryable().AsNoTracking();

        if (query.EmployeeId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeId == query.EmployeeId.Value);
        }

        return await queryable.OrderBy(e => e.CommunicationHistoryId)
                              .ProjectTo<CommunicationHistoryEmployeeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}