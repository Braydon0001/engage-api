// auto-generated
namespace Engage.Application.Services.SupplierSubContracts.Queries;

public class SupplierSubContractVmQuery : IRequest<SupplierSubContractVm>
{
    public int Id { get; set; }
}

public class SupplierSubContractVmHandler : VmQueryHandler, IRequestHandler<SupplierSubContractVmQuery, SupplierSubContractVm>
{
    public SupplierSubContractVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSubContractVm> Handle(SupplierSubContractVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSubContracts.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SupplierContract)
                             .Include(e => e.SupplierSubContractType);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierSubContractId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierSubContractVm>(entity);
    }
}