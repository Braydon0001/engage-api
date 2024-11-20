// auto-generated
namespace Engage.Application.Services.SupplierSubContractDetails.Queries;

public class SupplierSubContractDetailVmQuery : IRequest<SupplierSubContractDetailVm>
{
    public int Id { get; set; }
}

public class SupplierSubContractDetailVmHandler : VmQueryHandler, IRequestHandler<SupplierSubContractDetailVmQuery, SupplierSubContractDetailVm>
{
    public SupplierSubContractDetailVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSubContractDetailVm> Handle(SupplierSubContractDetailVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSubContractDetails.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SupplierSubContractType).Include(e => e.SupplierSubContractDetailType);

        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierSubContractDetailId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierSubContractDetailVm>(entity);
    }
}