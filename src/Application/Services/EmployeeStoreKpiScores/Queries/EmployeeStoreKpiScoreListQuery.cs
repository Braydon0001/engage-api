namespace Engage.Application.Services.EmployeeStoreKpiScores.Queries;

public class EmployeeStoreKpiScoreListQuery : IRequest<List<EmployeeStoreKpiScoreDto>>
{
    // Query property. Used for filtering.
    public int? EmployeeId { get; init; }
}

public record EmployeeStoreKpiScoreListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EmployeeStoreKpiScoreListQuery, List<EmployeeStoreKpiScoreDto>>
{
    public async Task<List<EmployeeStoreKpiScoreDto>> Handle(EmployeeStoreKpiScoreListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EmployeeStoreKpiScores.AsQueryable().AsNoTracking();

        // Filter the queryable
        if (query.EmployeeId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeId == query.EmployeeId.Value);
        }

        return await queryable.OrderByDescending(e => e.EmployeeStoreKpiScoreId)
                              .ProjectTo<EmployeeStoreKpiScoreDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}