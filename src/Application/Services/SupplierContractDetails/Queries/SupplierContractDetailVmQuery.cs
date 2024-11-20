// auto-generated
namespace Engage.Application.Services.SupplierContractDetails.Queries;

public class SupplierContractDetailVmQuery : IRequest<SupplierContractDetailVm>
{
    public int Id { get; set; }
}

public class SupplierContractDetailVmHandler : VmQueryHandler, IRequestHandler<SupplierContractDetailVmQuery, SupplierContractDetailVm>
{
    public SupplierContractDetailVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractDetailVm> Handle(SupplierContractDetailVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractDetails.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SupplierContract)
                             .Include(e => e.SupplierContractDetailType)
                             .Include(e => e.EngageRegion);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierContractDetailId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierContractDetailVm>(entity);
    }
}