namespace Engage.Application.Services.CategorySubGroups.Queries;

public class CategorySubGroupOptionQuery : IRequest<List<CategorySubGroupOption>>
{ 

}

public record CategorySubGroupOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategorySubGroupOptionQuery, List<CategorySubGroupOption>>
{
    public async Task<List<CategorySubGroupOption>> Handle(CategorySubGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategorySubGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CategorySubGroupOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}