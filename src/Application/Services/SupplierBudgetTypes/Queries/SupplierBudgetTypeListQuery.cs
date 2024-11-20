// auto-generated
namespace Engage.Application.Services.SupplierBudgetTypes.Queries;

public class SupplierBudgetTypeListQuery : IRequest<List<SupplierBudgetTypeDto>>
{

}

public class SupplierBudgetTypeListHandler : ListQueryHandler, IRequestHandler<SupplierBudgetTypeListQuery, List<SupplierBudgetTypeDto>>
{
    public SupplierBudgetTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierBudgetTypeDto>> Handle(SupplierBudgetTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierBudgetTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierBudgetTypeId)
                              .ProjectTo<SupplierBudgetTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}