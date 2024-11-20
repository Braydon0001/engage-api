// auto-generated
namespace Engage.Application.Services.StoreAssetConditions.Queries;

public class StoreAssetConditionOptionListQuery : IRequest<List<StoreAssetConditionOption>>
{

}

public class StoreAssetConditionOptionListHandler : ListQueryHandler, IRequestHandler<StoreAssetConditionOptionListQuery, List<StoreAssetConditionOption>>
{
    public StoreAssetConditionOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<StoreAssetConditionOption>> Handle(StoreAssetConditionOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreAssetConditions.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.Disabled == false);

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<StoreAssetConditionOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}