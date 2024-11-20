using Engage.Application.Services.CategoryTargets.Queries;

namespace Engage.Application.Services.CategoryTargetStores.Queries;

public class CategoryTargetStoreByStoreQuery : IRequest<List<CategoryTargetDto>>
{
    public int StoreId { get; set; }
}

public record CategoryTargetStoreByStoreHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetStoreByStoreQuery, List<CategoryTargetDto>>
{
    public async Task<List<CategoryTargetDto>> Handle(CategoryTargetStoreByStoreQuery query, CancellationToken cancellationToken)
    {

        var CategoryTargetIds = await Context.CategoryTargetStores.Where(e => e.StoreId == query.StoreId).Select(c => c.CategoryTargetId).ToListAsync(cancellationToken);


        var entities = Context.CategoryTargets.AsQueryable().AsNoTracking().Where(e => CategoryTargetIds.Contains(e.CategoryTargetId) && e.Disabled == false);



        return await entities
            .OrderBy(s => s.CategoryTargetId)
            .ProjectTo<CategoryTargetDto>(Mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

    }
}
