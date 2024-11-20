using Engage.Application.Services.EngageCategories.Models;

namespace Engage.Application.Services.EngageCategories.Queries;

public class EngageCategoryOptionQuery : IRequest<List<EngageCategoryOptionDto>>
{
}
public record EngageCategoryOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageCategoryOptionQuery, List<EngageCategoryOptionDto>>
{
    public async Task<List<EngageCategoryOptionDto>> Handle(EngageCategoryOptionQuery request, CancellationToken cancellationToken)
    {
        var queryable = Context.EngageCategories.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Id)
                        .ProjectTo<EngageCategoryOptionDto>(Mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
    }
}