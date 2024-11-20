// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersionTypes.Queries;

public class SupplierBudgetVersionTypeOptionListQuery : IRequest<List<SupplierBudgetVersionTypeOption>>
{ 

}

public class SupplierBudgetVersionTypeOptionListHandler : ListQueryHandler, IRequestHandler<SupplierBudgetVersionTypeOptionListQuery, List<SupplierBudgetVersionTypeOption>>
{
    public SupplierBudgetVersionTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierBudgetVersionTypeOption>> Handle(SupplierBudgetVersionTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierBudgetVersionTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierBudgetVersionTypeId)
                              .ProjectTo<SupplierBudgetVersionTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}