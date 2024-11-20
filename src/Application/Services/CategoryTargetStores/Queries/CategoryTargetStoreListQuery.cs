namespace Engage.Application.Services.CategoryTargetStores.Queries;

public class CategoryTargetStoreListQuery : IRequest<List<CategoryTargetStoreDto>>
{

}

public record CategoryTargetStoreListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetStoreListQuery, List<CategoryTargetStoreDto>>
{
    public async Task<List<CategoryTargetStoreDto>> Handle(CategoryTargetStoreListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryTargetStores.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CategoryTargetStoreId)
                              .ProjectTo<CategoryTargetStoreDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}