// auto-generated
namespace Engage.Application.Services.EngageDepartmentCategories.Queries;

public class EngageDepartmentCategoryListQuery : IRequest<List<EngageDepartmentCategoryDto>>
{
    public int? EngageDepartmentId { get; set; }
}

public class EngageDepartmentCategoryListHandler : ListQueryHandler, IRequestHandler<EngageDepartmentCategoryListQuery, List<EngageDepartmentCategoryDto>>
{
    public EngageDepartmentCategoryListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EngageDepartmentCategoryDto>> Handle(EngageDepartmentCategoryListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EngageDepartmentCategories.AsQueryable().AsNoTracking();

        if (query.EngageDepartmentId.HasValue)
        {
            queryable = queryable.Where(e => e.EngageDepartmentId == query.EngageDepartmentId.Value);
        }

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EngageDepartmentCategoryDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}