// auto-generated
namespace Engage.Application.Services.SupplierAllowances.Queries;

public class SupplierAllowanceVmQuery : IRequest<SupplierAllowanceVm>
{
    public int Id { get; set; }
}

public class SupplierAllowanceVmHandler : VmQueryHandler, IRequestHandler<SupplierAllowanceVmQuery, SupplierAllowanceVm>
{
    public SupplierAllowanceVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierAllowanceVm> Handle(SupplierAllowanceVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierAllowances.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Supplier)
                             .Include(e => e.SalesLead);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierAllowanceId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierAllowanceVm>(entity);
    }
}