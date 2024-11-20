namespace Engage.Application.Services.EngageSubRegions.Queries;

public class EngageSubRegionListQuery : IRequest<List<EngageSubRegionDto>>
{
    public int? EngageRegionId { get; set; }
}

public record EngageSubRegionListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageSubRegionListQuery, List<EngageSubRegionDto>>
{
    public async Task<List<EngageSubRegionDto>> Handle(EngageSubRegionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.EngageSubRegions.AsQueryable().AsNoTracking();

        if (query.EngageRegionId.HasValue)
        {
            queryable = queryable.Where(e => e.EngageRegionId == query.EngageRegionId.Value);
        }

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EngageSubRegionDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}