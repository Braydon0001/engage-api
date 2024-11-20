using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.Stores.Queries;

public class StoreOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public bool IsRegional { get; set; }
    public int? RegionId { get; set; }
}

public class StoreOptionsQueryHandler : IRequestHandler<StoreOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;
    private readonly IUserService _userService;

    public StoreOptionsQueryHandler(IAppDbContext context, IMediator mediator, IUserService userService)
    {
        _context = context;
        _mediator = mediator;
        _userService = userService;
    }

    public async Task<List<OptionDto>> Handle(StoreOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.Stores.AsQueryable();

        if (request.RegionId.HasValue)
        {
            queryable = queryable.Where(e => e.EngageRegionId == request.RegionId);
        }

        if (request.IsRegional)
        {
            var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

            queryable = queryable.Where(e => engageRegionIds.Contains(e.EngageRegionId));
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

