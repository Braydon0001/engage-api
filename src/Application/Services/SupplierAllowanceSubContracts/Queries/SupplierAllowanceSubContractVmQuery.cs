namespace Engage.Application.Services.SupplierAllowanceSubContracts.Queries;

public class SupplierAllowanceSubContractVmQuery : IRequest<SupplierAllowanceSubContractVm>
{
    public int Id { get; set; }
}

public class SupplierAllowanceSubContractVmHandler : VmQueryHandler, IRequestHandler<SupplierAllowanceSubContractVmQuery, SupplierAllowanceSubContractVm>
{
    public SupplierAllowanceSubContractVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierAllowanceSubContractVm> Handle(SupplierAllowanceSubContractVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierAllowanceSubContracts.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SupplierAllowanceContract);

        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierAllowanceSubContractId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierAllowanceSubContractVm>(entity);
    }
}