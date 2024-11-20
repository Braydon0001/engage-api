using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.ProjectStores.Queries;

public class ProjectStoreOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public bool IsRegional { get; set; }
    public int? StoreId { get; set; }
}

public class ProjectStoreOptionsQueryHandler : IRequestHandler<ProjectStoreOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _userService;

    public ProjectStoreOptionsQueryHandler(IAppDbContext context, IMediator mediator, IUserService userService)
    {
        _context = context;
        _mediator = mediator;
        _userService = userService;
    }

    public async Task<List<OptionDto>> Handle(ProjectStoreOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ProjectStores.AsNoTracking().AsQueryable();

        if (request.StoreId.HasValue)
        {
            queryable = queryable.Where(e => e.StoreId == request.StoreId);
        }

        if (request.IsRegional)
        {
            var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

            queryable = queryable.Where(e => engageRegionIds.Contains(e.Store.EngageRegionId));
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{request.Search}%"));
        }

        return await queryable.Where(e => e.Disabled == false)
                              .Select(e => new OptionDto { Id = e.ProjectId, Name = e.Name + " - " + e.Store.Name })
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}

