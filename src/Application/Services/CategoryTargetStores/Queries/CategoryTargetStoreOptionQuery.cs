namespace Engage.Application.Services.CategoryTargetStores.Queries;

public class CategoryTargetStoreOptionQuery : IRequest<List<CategoryTargetStoreOption>>
{ 

}

public record CategoryTargetStoreOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetStoreOptionQuery, List<CategoryTargetStoreOption>>
{
    public async Task<List<CategoryTargetStoreOption>> Handle(CategoryTargetStoreOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryTargetStores.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CategoryTargetStoreId)
                              .ProjectTo<CategoryTargetStoreOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}