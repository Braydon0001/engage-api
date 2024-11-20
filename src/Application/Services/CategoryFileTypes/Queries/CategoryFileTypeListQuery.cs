namespace Engage.Application.Services.CategoryFileTypes.Queries;

public class CategoryFileTypeListQuery : IRequest<List<CategoryFileTypeDto>>
{

}

public record CategoryFileTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryFileTypeListQuery, List<CategoryFileTypeDto>>
{
    public async Task<List<CategoryFileTypeDto>> Handle(CategoryFileTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryFileTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CategoryFileTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}