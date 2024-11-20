namespace Engage.Application.Services.CategoryStoreGroups.Queries;

public class CategoryStoreGroupListQuery : IRequest<List<CategoryStoreGroupDto>>
{

}

public record CategoryStoreGroupListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryStoreGroupListQuery, List<CategoryStoreGroupDto>>
{
    public async Task<List<CategoryStoreGroupDto>> Handle(CategoryStoreGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryStoreGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CategoryStoreGroupId)
                              .ProjectTo<CategoryStoreGroupDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}