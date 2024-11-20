namespace Engage.Application.Services.Employees.Queries;

public class RegionEmployeeOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int EngageRegionId { get; set; }
}

public class RegionEmployeeOptionsQueryHandler : IRequestHandler<RegionEmployeeOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public RegionEmployeeOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(RegionEmployeeOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Employees.Where(e => e.EmployeeTypeId == (int)EmployeeTypeId.Employee)
                                          .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => (EF.Functions.Like(e.FirstName, $"%{request.Search}%"))
                                                || (EF.Functions.Like(e.LastName, $"%{request.Search}%"))
                                                || (EF.Functions.Like(e.Code, $"%{request.Search}%"))
                                                );
        }

        if (request.EngageRegionId != 0)
        {
            queryable = queryable.Where(e => e.EmployeeRegions.Select(e => e.EngageRegionId).Contains(request.EngageRegionId));
        }

        return await queryable.Where(e => e.Disabled == false)
                                  .Select(e => new OptionDto { Id = e.EmployeeId, Name = e.FirstName + " " + e.LastName + " - " + e.Code })
                                  .Take(100)
                                  .OrderBy(e => e.Name)
                                  .ToListAsync(cancellationToken);
    }
}