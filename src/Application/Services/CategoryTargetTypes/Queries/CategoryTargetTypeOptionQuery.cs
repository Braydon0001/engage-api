namespace Engage.Application.Services.CategoryTargetTypes.Queries;

public class CategoryTargetTypeOptionQuery : IRequest<List<CategoryTargetTypeOption>>
{ 

}

public record CategoryTargetTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CategoryTargetTypeOptionQuery, List<CategoryTargetTypeOption>>
{
    public async Task<List<CategoryTargetTypeOption>> Handle(CategoryTargetTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CategoryTargetTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CategoryTargetTypeId)
                              .ProjectTo<CategoryTargetTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}