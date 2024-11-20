namespace Engage.Application.Services.Claims.Queries;

public class ProjectClaimOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int ProjectId { get; set; }
}

public class ProjectClaimOptionsQueryHandler : IRequestHandler<ProjectClaimOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public ProjectClaimOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(ProjectClaimOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Claims.Where(c => (c.ClaimStatusId == (int)ClaimStatusId.Approved ||
                                                   c.ClaimStatusId == (int)ClaimStatusId.Paid ||
                                                   c.ClaimStatusId == (int)ClaimStatusId.Unapproved)).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => (EF.Functions.Like(e.ClaimNumber, $"%{request.Search}%"))
                                            || (EF.Functions.Like(e.ClaimReference, $"%{request.Search}%"))
                                            );
        }

        if (request.ProjectId != 0)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(v => v.ProjectId == request.ProjectId, cancellationToken);
            var projectStore = await _context.ProjectStores.FirstOrDefaultAsync(v => v.ProjectId == request.ProjectId, cancellationToken);

            if (project.EngageRegionId.HasValue)
            {
                queryable = queryable.Where(e => e.Store.EngageRegionId == project.EngageRegionId);
            }

            if (projectStore != null)
            {
                queryable = queryable.Where(e => e.StoreId == projectStore.StoreId);
            }
        }

        return await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.ClaimId, Name = e.ClaimNumber + " - " + e.Store.Name })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}