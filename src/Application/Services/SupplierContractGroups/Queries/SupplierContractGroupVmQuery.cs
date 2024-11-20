// auto-generated
namespace Engage.Application.Services.SupplierContractGroups.Queries;

public class SupplierContractGroupVmQuery : IRequest<SupplierContractGroupVm>
{
    public int Id { get; set; }
}

public class SupplierContractGroupVmHandler : VmQueryHandler, IRequestHandler<SupplierContractGroupVmQuery, SupplierContractGroupVm>
{
    public SupplierContractGroupVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractGroupVm> Handle(SupplierContractGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractGroups.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierContractGroupId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierContractGroupVm>(entity);
    }
}