namespace Engage.Application.Services.CategoryTargetTypes.Queries;

public class CategoryTargetTypeListQuery : IRequest<List<CategoryTargetTypeDto>>
{

}

public record CategoryTargetTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetTypeListQuery, List<CategoryTargetTypeDto>>
{
    public async Task<List<CategoryTargetTypeDto>> Handle(CategoryTargetTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryTargetTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CategoryTargetTypeId)
                              .ProjectTo<CategoryTargetTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}