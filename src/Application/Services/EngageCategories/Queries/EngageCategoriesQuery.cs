using Engage.Application.Services.EngageCategories.Models;

namespace Engage.Application.Services.EngageCategories.Queries;

public class EngageCategoriesQuery : GetQuery, IRequest<ListResult<EngageCategoryDto>>
{
}

public class EngageCategoriesQueryHandler : BaseQueryHandler, IRequestHandler<EngageCategoriesQuery, ListResult<EngageCategoryDto>>
{
    public EngageCategoriesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EngageCategoryDto>> Handle(EngageCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EngageCategories.OrderBy(e => e.Name)
                                                      .ProjectTo<EngageCategoryDto>(_mapper.ConfigurationProvider)
                                                      .ToListAsync(cancellationToken);

        return new ListResult<EngageCategoryDto>(entities);
    }
}
