namespace Engage.Application.Services.CategoryFileTypes.Queries;

public class CategoryFileTypeOptionQuery : IRequest<List<CategoryFileTypeOption>>
{ 

}

public record CategoryFileTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryFileTypeOptionQuery, List<CategoryFileTypeOption>>
{
    public async Task<List<CategoryFileTypeOption>> Handle(CategoryFileTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryFileTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CategoryFileTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}