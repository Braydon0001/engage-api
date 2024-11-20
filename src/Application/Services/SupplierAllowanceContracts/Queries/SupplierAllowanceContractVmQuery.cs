namespace Engage.Application.Services.SupplierAllowanceContracts.Queries;

public class SupplierAllowanceContractVmQuery : IRequest<SupplierAllowanceContractVm>
{
    public int Id { get; set; }
}

public class SupplierAllowanceContractVmHandler : VmQueryHandler, IRequestHandler<SupplierAllowanceContractVmQuery, SupplierAllowanceContractVm>
{
    public SupplierAllowanceContractVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierAllowanceContractVm> Handle(SupplierAllowanceContractVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierAllowanceContracts.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Supplier)
                             .Include(e => e.SupplierSalesLead);

        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierAllowanceContractId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierAllowanceContractVm>(entity);
    }
}