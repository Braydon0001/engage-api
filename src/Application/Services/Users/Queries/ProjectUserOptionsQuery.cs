namespace Engage.Application.Services.Users.Queries;

public class ProjectUserOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int ProjectId { get; set; }
}

public class ProjectUserOptionsQueryHandler : IRequestHandler<ProjectUserOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public ProjectUserOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(ProjectUserOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Users.AsQueryable();

        var regionIds = new List<int>();
        var storeIds = new List<int>();
        var jobTitleIds = new List<int>();

        if (request.ProjectId != 0)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(v => v.ProjectId == request.ProjectId);
            var projectTagEngageRegionIds = await _context.ProjectProjectTagEngageRegions.Where(e => e.ProjectId == request.ProjectId)
                                                                                         .Select(e => e.EngageRegionId)
                                                                                         .ToListAsync(cancellationToken);

            var projectStore = await _context.ProjectStores.FirstOrDefaultAsync(v => v.ProjectId == request.ProjectId, cancellationToken);
            var projectTagStoreIds = await _context.ProjectProjectTagStores.Where(e => e.ProjectId == request.ProjectId)
                                                                           .Select(e => e.StoreId)
                                                                           .ToListAsync(cancellationToken);

            var projectTagJobTitleIds = await _context.ProjectProjectTagEmployeeJobTitles.Where(e => e.ProjectId == request.ProjectId)
                                                                                         .Select(e => e.EmployeeJobTitleId)
                                                                                         .ToListAsync(cancellationToken);

            if (project.EngageRegionId.HasValue)
            {
                regionIds.Add(project.EngageRegionId.Value);
            }

            if (projectTagEngageRegionIds.Count > 0)
            {
                regionIds.AddRange(projectTagEngageRegionIds);
            }

            if (projectStore != null)
            {
                storeIds.Add(projectStore.StoreId);
            }

            if (projectTagStoreIds.Count > 0)
            {
                storeIds.AddRange(projectTagStoreIds);
            }

            if (regionIds.Count > 0)
            {
                regionIds = regionIds.Distinct().ToList();
            }

            if (storeIds.Count > 0)
            {
                storeIds = storeIds.Distinct().ToList();
            }

            if (projectTagJobTitleIds.Count > 0)
            {
                jobTitleIds.AddRange(projectTagJobTitleIds);
            }

            var employeeUserIds = await _context.EmployeeStores.Where(e => storeIds.Contains(e.StoreId))
                                                               .Select(e => e.Employee.UserId)
                                                               .Distinct()
                                                               .ToListAsync(cancellationToken);

            var regionUserIds = await _context.EmployeeRegions.Where(e => regionIds.Contains(e.EngageRegionId))
                                                              .Select(e => e.Employee.UserId)
                                                              .Distinct()
                                                              .ToListAsync(cancellationToken);

            var jobTitleUserIds = await _context.EmployeeEmployeeJobTitles.Where(e => jobTitleIds.Contains(e.EmployeeJobTitleId))
                                                                          .Select(e => e.Employee.UserId)
                                                                          .Distinct()
                                                                          .ToListAsync(cancellationToken);

            var userIds = employeeUserIds.Union(regionUserIds)
                                         .Union(jobTitleUserIds)
                                         .Distinct()
                                         .ToList();

            if (userIds.Count > 0)
            {
                queryable = queryable.Where(e => userIds.Contains(e.UserId));
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.FirstName, $"%{request.Search}%")
                                                || EF.Functions.Like(e.LastName, $"%{request.Search}%")
                                                || EF.Functions.Like(e.Email, $"%{request.Search}%")
                                              );
        }

        return await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.UserId, Name = e.FirstName + " " + e.LastName + " - " + e.Email })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}