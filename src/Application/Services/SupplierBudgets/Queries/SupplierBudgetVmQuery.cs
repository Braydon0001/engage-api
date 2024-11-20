// auto-generated
namespace Engage.Application.Services.SupplierBudgets.Queries;

public class SupplierBudgetVmQuery : IRequest<SupplierBudgetVm>
{
    public int Id { get; set; }
}

public class SupplierBudgetVmHandler : VmQueryHandler, IRequestHandler<SupplierBudgetVmQuery, SupplierBudgetVm>
{
    public SupplierBudgetVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierBudgetVm> Handle(SupplierBudgetVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierBudgets.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SupplierBudgetVersion)
                             .ThenInclude(e => e.SupplierBudgetVersionType)
                             .Include(e => e.SupplierBudgetVersion.SupplierPeriod)
                             .Include(e => e.SupplierBudgetType)
                             .Include(e => e.Supplier)
                             .Include(e => e.SupplierContractDetail)
                             .ThenInclude(e => e.SupplierContract)
                             .Include(e => e.EngageRegion);

        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierBudgetId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierBudgetVm>(entity);
    }
}