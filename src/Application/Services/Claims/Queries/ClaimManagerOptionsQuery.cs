namespace Engage.Application.Services.Claims.Queries;

public class ClaimManagerOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int StoreId { get; set; }
}

public class ClaimManagerOptionsQueryHandler : IRequestHandler<ClaimManagerOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public ClaimManagerOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(ClaimManagerOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Stores.Where(e => e.StoreId == request.StoreId)
                                            .Join(_context.EngageRegionClaimManagers.Where(e => e.Disabled == false),
                                                  store => store.EngageRegionId,
                                                  engageRegionClaimManager => engageRegionClaimManager.EngageRegionId,
                                                  (store, engageRegionClaimManager) => engageRegionClaimManager)
                                            .Include(e => e.User)
                                            .Where(e => e.User.Disabled == false)
                                            .OrderBy(e => e.User.FullName)
                                            .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.UserId, e.User.FullName))
                       .ToList();
    }
}
