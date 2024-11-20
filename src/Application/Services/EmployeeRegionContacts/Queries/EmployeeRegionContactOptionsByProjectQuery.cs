namespace Engage.Application.Services.EmployeeRegionContacts.Queries;

public class EmployeeRegionContactOptionsByProjectQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int ProjectId { get; set; }
}

public class EmployeeRegionContactOptionsByProjectQueryHandler : IRequestHandler<EmployeeRegionContactOptionsByProjectQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public EmployeeRegionContactOptionsByProjectQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(EmployeeRegionContactOptionsByProjectQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.EmployeeRegionContacts.Where(e => e.Disabled == false && e.Deleted == false).AsQueryable();

        var regionIds = new List<int>();

        if (request.ProjectId != 0)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(v => v.ProjectId == request.ProjectId, cancellationToken);
            var projectStore = await _context.ProjectStores.Include(e => e.Store).FirstOrDefaultAsync(v => v.ProjectId == request.ProjectId, cancellationToken);

            if (project.EngageRegionId.HasValue)
            {
                regionIds.Add(project.EngageRegionId.Value);
            }

            if (projectStore != null)
            {
                regionIds.Add(projectStore.Store.EngageRegionId);
            }

            var projectStoreRegionIds = await _context.ProjectProjectTagStores.Where(e => e.ProjectId == request.ProjectId).Select(e => e.Store.EngageRegionId).ToListAsync(cancellationToken);
            if (projectStoreRegionIds.Count > 0)
            {
                regionIds.AddRange(projectStoreRegionIds);
            }

            regionIds = regionIds.Distinct().ToList();

            if (regionIds.Count > 0)
            {
                queryable = queryable.Where(e => regionIds.Contains(e.EngageRegionId));
            }
            else
            {
                return new List<OptionDto>();
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Employee.FirstName, $"%{request.Search}%")
                                                || EF.Functions.Like(e.Employee.LastName, $"%{request.Search}%")
                                                || EF.Functions.Like(e.Employee.EmailAddress1, $"%{request.Search}%")
                                              );
        }

        return await queryable.Select(e => new OptionDto { Id = e.EmployeeRegionContactId, Name = e.Employee.FirstName + " " + e.Employee.LastName + " - " + e.EngageRegion.Name })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}

