// auto-generated
namespace Engage.Application.Services.SupplierBudgetTypes.Queries;

public class SupplierBudgetTypeOptionListQuery : IRequest<List<SupplierBudgetTypeOption>>
{ 

}

public class SupplierBudgetTypeOptionListHandler : ListQueryHandler, IRequestHandler<SupplierBudgetTypeOptionListQuery, List<SupplierBudgetTypeOption>>
{
    public SupplierBudgetTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierBudgetTypeOption>> Handle(SupplierBudgetTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierBudgetTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierBudgetTypeId)
                              .ProjectTo<SupplierBudgetTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}