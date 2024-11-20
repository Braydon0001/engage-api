namespace Engage.Application.Services.ClaimFloats.Queries;

public class ClaimFloatOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int? EngageRegionId { get; set; }
    public int? SupplierId { get; set; }
    public int? StoreId { get; set; }
    public bool ExcludeDisabled { get; set; }
}

public class ClaimFloatOptionsQueryHandler : IRequestHandler<ClaimFloatOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;
    private readonly IUserService _user;

    public ClaimFloatOptionsQueryHandler(IAppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<List<OptionDto>> Handle(ClaimFloatOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ClaimFloats.AsQueryable();

        if (request.ExcludeDisabled)
        {
            queryable = queryable.Where(e => e.Disabled == false);
        }

        if (request.EngageRegionId.HasValue)
        {
            queryable = queryable.Where(e => e.EngageRegionId == request.EngageRegionId);
        }

        if (request.SupplierId.HasValue)
        {
            queryable = queryable.Where(e => e.SupplierId == request.SupplierId);
        }

        if (request.StoreId.HasValue)
        {
            var store = await _context.Stores.Where(e => e.StoreId == request.StoreId.Value).FirstOrDefaultAsync(cancellationToken);
            if (store != null)
            {
                queryable = queryable.Where(e => e.EngageRegionId == store.EngageRegionId);
            }
        }

        if (!_user.IsHostSupplier)
        {
            queryable = queryable.Where(e => e.SupplierId == _user.SupplierId);
        }

        var entities = await queryable.OrderBy(e => e.StartDate)
                                  .ThenBy(e => e.Title)
                                  .Select(e => new OptionDto { Id = e.ClaimFloatId, Name = e.Title })
                                  .ToListAsync(cancellationToken);

        return new List<OptionDto>(entities);
    }
}
