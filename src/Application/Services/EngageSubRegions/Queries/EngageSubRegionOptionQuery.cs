namespace Engage.Application.Services.EngageSubRegions.Queries;

public class EngageSubRegionOptionQuery : IRequest<List<EngageSubRegionOption>>
{
    public int? EngageRegionId { get; set; }
    public string EngageRegionIds { get; set; }
}

public record EngageSubRegionOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageSubRegionOptionQuery, List<EngageSubRegionOption>>
{
    public async Task<List<EngageSubRegionOption>> Handle(EngageSubRegionOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EngageSubRegions.AsQueryable().AsNoTracking();

        if (query.EngageRegionIds.IsNotNullOrWhiteSpace())
        {
            List<int> regionIds = query.EngageRegionIds.Split(',').Select(int.Parse).ToList();
            queryable = queryable.Where(e => regionIds.Contains(e.EngageRegionId));
        }

        if (query.EngageRegionId.HasValue)
        {
            queryable = queryable.Where(e => e.EngageRegionId == query.EngageRegionId.Value);
        }

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EngageSubRegionOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}