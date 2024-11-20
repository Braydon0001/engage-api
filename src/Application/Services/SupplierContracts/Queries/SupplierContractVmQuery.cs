// auto-generated
namespace Engage.Application.Services.SupplierContracts.Queries;

public class SupplierContractVmQuery : IRequest<SupplierContractVm>
{
    public int Id { get; set; }
}

public class SupplierContractVmHandler : VmQueryHandler, IRequestHandler<SupplierContractVmQuery, SupplierContractVm>
{
    public SupplierContractVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractVm> Handle(SupplierContractVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContracts.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Supplier)
                             .Include(e => e.SupplierContractType)
                             .Include(e => e.SupplierContractGroup)
                             .Include(e => e.SupplierContractSubGroup)
                             .Include(e => e.SupplierContact);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierContractId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierContractVm>(entity);
    }
}