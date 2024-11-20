namespace Engage.Application.Services.CategoryTargets.Queries;

public class CategoryTargetQuery : IRequest<List<CategoryTargetDto>>
{

}
public record CategoryTargetListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetQuery, List<CategoryTargetDto>>
{
    public async Task<List<CategoryTargetDto>> Handle(CategoryTargetQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryTargets.Where(e => e.Disabled == false).AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.CategoryTargetId)
                              .ProjectTo<CategoryTargetDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}