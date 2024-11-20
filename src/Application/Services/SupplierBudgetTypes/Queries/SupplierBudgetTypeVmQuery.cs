// auto-generated
namespace Engage.Application.Services.SupplierBudgetTypes.Queries;

public class SupplierBudgetTypeVmQuery : IRequest<SupplierBudgetTypeVm>
{
    public int Id { get; set; }
}

public class SupplierBudgetTypeVmHandler : VmQueryHandler, IRequestHandler<SupplierBudgetTypeVmQuery, SupplierBudgetTypeVm>
{
    public SupplierBudgetTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierBudgetTypeVm> Handle(SupplierBudgetTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierBudgetTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierBudgetTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierBudgetTypeVm>(entity);
    }
}