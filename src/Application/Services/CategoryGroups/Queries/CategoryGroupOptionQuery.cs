namespace Engage.Application.Services.CategoryGroups.Queries;

public class CategoryGroupOptionQuery : IRequest<List<CategoryGroupOption>>
{ 

}

public record CategoryGroupOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryGroupOptionQuery, List<CategoryGroupOption>>
{
    public async Task<List<CategoryGroupOption>> Handle(CategoryGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CategoryGroupOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}