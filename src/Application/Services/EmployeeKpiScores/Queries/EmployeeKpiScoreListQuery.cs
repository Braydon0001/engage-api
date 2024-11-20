namespace Engage.Application.Services.EmployeeKpiScores.Queries;

public class EmployeeKpiScoreListQuery : IRequest<List<EmployeeKpiScoreDto>>
{
    // Query property. Used for filtering.
    public int? EmployeeId { get; init; }
}

public record EmployeeKpiScoreListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeKpiScoreListQuery, List<EmployeeKpiScoreDto>>
{
    public async Task<List<EmployeeKpiScoreDto>> Handle(EmployeeKpiScoreListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EmployeeKpiScores.AsQueryable().AsNoTracking();

        // Filter the queryable
        if (query.EmployeeId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeId == query.EmployeeId.Value);
        }

        return await queryable.OrderByDescending(e => e.EmployeeKpiScoreId)
                              .ProjectTo<EmployeeKpiScoreDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}