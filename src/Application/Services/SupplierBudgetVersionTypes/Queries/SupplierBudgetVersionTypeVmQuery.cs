// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersionTypes.Queries;

public class SupplierBudgetVersionTypeVmQuery : IRequest<SupplierBudgetVersionTypeVm>
{
    public int Id { get; set; }
}

public class SupplierBudgetVersionTypeVmHandler : VmQueryHandler, IRequestHandler<SupplierBudgetVersionTypeVmQuery, SupplierBudgetVersionTypeVm>
{
    public SupplierBudgetVersionTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierBudgetVersionTypeVm> Handle(SupplierBudgetVersionTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierBudgetVersionTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierBudgetVersionTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierBudgetVersionTypeVm>(entity);
    }
}