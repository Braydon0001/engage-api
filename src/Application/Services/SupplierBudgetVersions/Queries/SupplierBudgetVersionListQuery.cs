// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersions.Queries;

public class SupplierBudgetVersionListQuery : IRequest<List<SupplierBudgetVersionDto>>
{

}

public class SupplierBudgetVersionListHandler : ListQueryHandler, IRequestHandler<SupplierBudgetVersionListQuery, List<SupplierBudgetVersionDto>>
{
    public SupplierBudgetVersionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierBudgetVersionDto>> Handle(SupplierBudgetVersionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierBudgetVersions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierBudgetVersionId)
                              .ProjectTo<SupplierBudgetVersionDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}