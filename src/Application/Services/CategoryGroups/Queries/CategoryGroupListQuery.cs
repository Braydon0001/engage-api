namespace Engage.Application.Services.CategoryGroups.Queries;

public class CategoryGroupListQuery : IRequest<List<CategoryGroupDto>>
{

}

public record CategoryGroupListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryGroupListQuery, List<CategoryGroupDto>>
{
    public async Task<List<CategoryGroupDto>> Handle(CategoryGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CategoryGroupDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}