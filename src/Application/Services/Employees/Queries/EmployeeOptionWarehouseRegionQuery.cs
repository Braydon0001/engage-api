namespace Engage.Application.Services.Employees.Queries;

public class EmployeeOptionWarehouseRegionQuery : IRequest<List<OptionDto>>
{
    public int ProductWarehouseId { get; set; }
    public string Search { get; set; }
}
public class EmployeeOptionWarehouseRegionHandler : BaseQueryHandler, IRequestHandler<EmployeeOptionWarehouseRegionQuery, List<OptionDto>>
{
    public EmployeeOptionWarehouseRegionHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<OptionDto>> Handle(EmployeeOptionWarehouseRegionQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.Employees.Where(e => e.EmployeeTypeId == (int)EmployeeTypeId.Employee)
                                          .AsNoTracking()
                                          .IgnoreQueryFilters()
                                          .AsQueryable();

        var engageRegionIds = await _context.ProductWarehouseRegions
                                      .Where(e => e.ProductWarehouseId == query.ProductWarehouseId)
                                      .Select(e => e.EngageRegionId)
                                      .ToListAsync(cancellationToken);

        if (engageRegionIds.Count > 0 && !engageRegionIds.Contains(7))
        {
            engageRegionIds.Add(7); //adding central users
            queryable = queryable
                                .Join(_context.EmployeeRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId)),
                                      employee => employee.EmployeeId,
                                      region => region.EmployeeId,
                                      (employee, region) => employee);
        }

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            queryable = queryable.Where(e => (EF.Functions.Like(e.FirstName, $"%{query.Search}%"))
                                            || (EF.Functions.Like(e.LastName, $"%{query.Search}%"))
                                            || (EF.Functions.Like(e.Code, $"%{query.Search}%"))
                                            );
        }

        var entities = await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.EmployeeId, Name = e.FirstName + " " + e.LastName + " - " + e.Code })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);

        return entities.DistinctBy(e => e.Id).ToList();
    }
}