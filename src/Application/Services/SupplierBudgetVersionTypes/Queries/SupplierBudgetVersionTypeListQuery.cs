// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersionTypes.Queries;

public class SupplierBudgetVersionTypeListQuery : IRequest<List<SupplierBudgetVersionTypeDto>>
{

}

public class SupplierBudgetVersionTypeListHandler : ListQueryHandler, IRequestHandler<SupplierBudgetVersionTypeListQuery, List<SupplierBudgetVersionTypeDto>>
{
    public SupplierBudgetVersionTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierBudgetVersionTypeDto>> Handle(SupplierBudgetVersionTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierBudgetVersionTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierBudgetVersionTypeId)
                              .ProjectTo<SupplierBudgetVersionTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}