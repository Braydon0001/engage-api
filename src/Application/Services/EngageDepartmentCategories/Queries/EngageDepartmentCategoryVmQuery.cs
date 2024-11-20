// auto-generated
namespace Engage.Application.Services.EngageDepartmentCategories.Queries;

public class EngageDepartmentCategoryVmQuery : IRequest<EngageDepartmentCategoryVm>
{
    public int Id { get; set; }
}

public class EngageDepartmentCategoryVmHandler : VmQueryHandler, IRequestHandler<EngageDepartmentCategoryVmQuery, EngageDepartmentCategoryVm>
{
    public EngageDepartmentCategoryVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageDepartmentCategoryVm> Handle(EngageDepartmentCategoryVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.EngageDepartmentCategories.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.EngageDepartment);

        var entity = await queryable.SingleOrDefaultAsync(e => e.Id == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<EngageDepartmentCategoryVm>(entity);
    }
}