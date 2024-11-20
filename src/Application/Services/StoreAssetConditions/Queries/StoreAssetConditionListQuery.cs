// auto-generated
namespace Engage.Application.Services.StoreAssetConditions.Queries;

public class StoreAssetConditionListQuery : IRequest<List<StoreAssetConditionDto>>
{

}

public class StoreAssetConditionListHandler : ListQueryHandler, IRequestHandler<StoreAssetConditionListQuery, List<StoreAssetConditionDto>>
{
    public StoreAssetConditionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<StoreAssetConditionDto>> Handle(StoreAssetConditionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreAssetConditions.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.Disabled == false);

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<StoreAssetConditionDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}