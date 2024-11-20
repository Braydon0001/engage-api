namespace Engage.Application.Services.CategorySubGroups.Queries;

public class CategorySubGroupListQuery : IRequest<List<CategorySubGroupDto>>
{

}

public record CategorySubGroupListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategorySubGroupListQuery, List<CategorySubGroupDto>>
{
    public async Task<List<CategorySubGroupDto>> Handle(CategorySubGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategorySubGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CategorySubGroupDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}