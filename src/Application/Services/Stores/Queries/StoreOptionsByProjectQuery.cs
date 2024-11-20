namespace Engage.Application.Services.Stores.Queries;

public class StoreOptionsByProjectQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int ProjectId { get; set; }
}

public class StoreOptionsByProjectQueryHandler : IRequestHandler<StoreOptionsByProjectQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public StoreOptionsByProjectQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(StoreOptionsByProjectQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Stores.AsQueryable();

        if (request.ProjectId != 0)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(v => v.ProjectId == request.ProjectId, cancellationToken);
            var projectStore = await _context.ProjectStores.Include(e => e.Store).FirstOrDefaultAsync(v => v.ProjectId == request.ProjectId, cancellationToken);

            if (project.EngageRegionId.HasValue)
            {
                queryable = queryable.Where(e => e.EngageRegionId == project.EngageRegionId);
            }

            if (projectStore != null)
            {
                queryable = queryable.Where(e => e.EngageRegionId == projectStore.Store.EngageRegionId);
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{request.Search}%"));
        }

        return await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.StoreId, Name = e.Name + " - " + e.EngageRegion.Name })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}

