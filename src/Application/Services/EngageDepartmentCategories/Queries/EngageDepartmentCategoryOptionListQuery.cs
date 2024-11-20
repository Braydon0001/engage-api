// auto-generated
namespace Engage.Application.Services.EngageDepartmentCategories.Queries;

public class EngageDepartmentCategoryOptionListQuery : IRequest<List<EngageDepartmentCategoryOption>>
{ 
    public int EngageDepartmentId { get; set; }
}

public class EngageDepartmentCategoryOptionListHandler : ListQueryHandler, IRequestHandler<EngageDepartmentCategoryOptionListQuery, List<EngageDepartmentCategoryOption>>
{
    public EngageDepartmentCategoryOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<EngageDepartmentCategoryOption>> Handle(EngageDepartmentCategoryOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EngageDepartmentCategories.AsQueryable().AsNoTracking();

        queryable = queryable.Where(e => e.EngageDepartmentId == query.EngageDepartmentId);
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<EngageDepartmentCategoryOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}