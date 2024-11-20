namespace Engage.Application.Services.CostCenters.Queries;

public class CostCenterOptionQuery : IRequest<List<CostCenterOption>>
{
    public int? EmployeeId { get; set; }
    public string EmployeeIds { get; set; }
}

public record CostCenterOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterOptionQuery, List<CostCenterOption>>
{
    public async Task<List<CostCenterOption>> Handle(CostCenterOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostCenters.AsQueryable().AsNoTracking();

        if (query.EmployeeIds.IsNotNullOrWhiteSpace())
        {
            List<int> empIds = query.EmployeeIds.Split(',').Select(int.Parse).ToList();
            var costCenterIds = await Context.CostCenterEmployees.Where(e => empIds.Contains(e.EmployeeId))
                                                                 .Select(e => e.CostCenterId)
                                                                 .ToListAsync(cancellationToken);

            queryable = queryable.Where(e => costCenterIds.Contains(e.CostCenterId));
        }

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CostCenterOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}