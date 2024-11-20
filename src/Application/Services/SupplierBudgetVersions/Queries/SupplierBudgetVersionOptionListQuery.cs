// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersions.Queries;

public class SupplierBudgetVersionOptionListQuery : IRequest<List<SupplierBudgetVersionOption>>
{ 

}

public class SupplierBudgetVersionOptionListHandler : ListQueryHandler, IRequestHandler<SupplierBudgetVersionOptionListQuery, List<SupplierBudgetVersionOption>>
{
    public SupplierBudgetVersionOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<SupplierBudgetVersionOption>> Handle(SupplierBudgetVersionOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierBudgetVersions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SupplierBudgetVersionId)
                              .ProjectTo<SupplierBudgetVersionOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}