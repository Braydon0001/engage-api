// auto-generated
namespace Engage.Application.Services.SupplierBudgets.Queries;

public class SupplierBudgetListQuery : IRequest<List<SupplierBudgetDto>>
{

}

public class SupplierBudgetListHandler : ListQueryHandler, IRequestHandler<SupplierBudgetListQuery, List<SupplierBudgetDto>>
{
    public SupplierBudgetListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierBudgetDto>> Handle(SupplierBudgetListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierBudgets.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierBudgetId)
                              .ProjectTo<SupplierBudgetDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}