// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersions.Queries;

public class SupplierBudgetVersionVmQuery : IRequest<SupplierBudgetVersionVm>
{
    public int Id { get; set; }
}

public class SupplierBudgetVersionVmHandler : VmQueryHandler, IRequestHandler<SupplierBudgetVersionVmQuery, SupplierBudgetVersionVm>
{
    public SupplierBudgetVersionVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierBudgetVersionVm> Handle(SupplierBudgetVersionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierBudgetVersions.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SupplierPeriod)
                             .Include(e => e.SupplierBudgetVersionType);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierBudgetVersionId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierBudgetVersionVm>(entity);
    }
}