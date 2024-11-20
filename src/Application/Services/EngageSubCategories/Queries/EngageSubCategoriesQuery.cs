using Engage.Application.Services.EngageSubCategories.Models;

namespace Engage.Application.Services.EngageSubCategories.Queries;

public class EngageSubCategoriesQuery : GetQuery, IRequest<ListResult<EngageSubCategoryDto>>
{
}

public class EngageSubCategoriesQueryHandler : BaseQueryHandler, IRequestHandler<EngageSubCategoriesQuery, ListResult<EngageSubCategoryDto>>
{
    public EngageSubCategoriesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EngageSubCategoryDto>> Handle(EngageSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EngageSubCategories.OrderBy(e => e.Name)
                                                         .ProjectTo<EngageSubCategoryDto>(_mapper.ConfigurationProvider)
                                                         .ToListAsync(cancellationToken);

        return new ListResult<EngageSubCategoryDto>(entities);
    }
}
