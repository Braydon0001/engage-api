// auto-generated
namespace Engage.Application.Services.SupplierContractSubGroups.Queries;

public class SupplierContractSubGroupVmQuery : IRequest<SupplierContractSubGroupVm>
{
    public int Id { get; set; }
}

public class SupplierContractSubGroupVmHandler : VmQueryHandler, IRequestHandler<SupplierContractSubGroupVmQuery, SupplierContractSubGroupVm>
{
    public SupplierContractSubGroupVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractSubGroupVm> Handle(SupplierContractSubGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractSubGroups.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SupplierContractGroup);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierContractSubGroupId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierContractSubGroupVm>(entity);
    }
}