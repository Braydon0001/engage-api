using Engage.Application.Services.EngageSubCategories.Models;

namespace Engage.Application.Services.EngageSubCategories.Queries;

public class EngageSubCategoryOptionQuery : IRequest<List<EngageSubCategoryOptionDto>>
{
}
public record EngageSubCategoryOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageSubCategoryOptionQuery, List<EngageSubCategoryOptionDto>>
{
    public async Task<List<EngageSubCategoryOptionDto>> Handle(EngageSubCategoryOptionQuery request, CancellationToken cancellationToken)
    {
        var queryable = Context.EngageSubCategories.AsNoTracking().AsQueryable();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EngageSubCategoryOptionDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}