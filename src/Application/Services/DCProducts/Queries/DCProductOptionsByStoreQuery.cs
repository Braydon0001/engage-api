namespace Engage.Application.Services.DCProducts.Queries;

public class DCProductOptionsByStoreQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int? StoreId { get; set; }
    public string EngageBrandIds { get; set; }
}

public class DCProductOptionsByStoreQueryHandler : BaseQueryHandler, IRequestHandler<DCProductOptionsByStoreQuery, List<OptionDto>>
{
    private readonly IUserService _user;

    public DCProductOptionsByStoreQueryHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<List<OptionDto>> Handle(DCProductOptionsByStoreQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.DCProducts.Where(e => e.Disabled == false && e.Deleted == false
                                              && e.EngageVariantProduct.Disabled == false
                                              && e.EngageVariantProduct.Deleted == false
                                              && e.EngageVariantProduct.EngageMasterProduct.Disabled == false
                                              && e.EngageVariantProduct.EngageMasterProduct.Deleted == false)
                                      .AsQueryable();

        if (query.StoreId.HasValue)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(e => e.StoreId == query.StoreId, cancellationToken)
                        ?? throw new Exception("Store Not Found");

            var dcAccounts = await _context.DCAccounts.Where(e => e.StoreId == query.StoreId).ToListAsync();
            var distributionCenterIds = dcAccounts.Select(e => e.DistributionCenterId).ToList();

            queryable = queryable.Where(e => distributionCenterIds.Contains(e.DistributionCenterId));
        }

        if (query.EngageBrandIds.IsNotNullOrWhiteSpace())
        {
            List<int> engageBrandIds = query.EngageBrandIds.Split(',').Select(int.Parse).ToList();

            queryable = queryable.Where(e => engageBrandIds.Contains(e.EngageVariantProduct.EngageMasterProduct.EngageBrandId));
        }

        return await queryable
                     .Where(e => (
                                 EF.Functions.Like(e.Code, $"%{query.Search}%") ||
                                 EF.Functions.Like(e.Name, $"%{query.Search}%") ||
                                 EF.Functions.Like(e.Code, $"%{query.Search}%") ||
                                 EF.Functions.Like(e.Name, $"%{query.Search}%")))
                     .Select(e => new OptionDto
                     {
                         Id = e.DCProductId,
                         Name = e.Name
                     })
                     .Take(100)
                     .OrderBy(e => e.Name)
                     .ToListAsync(cancellationToken);
    }
}
