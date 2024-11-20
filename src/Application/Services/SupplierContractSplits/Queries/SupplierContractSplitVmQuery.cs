// auto-generated
namespace Engage.Application.Services.SupplierContractSplits.Queries;

public class SupplierContractSplitVmQuery : IRequest<SupplierContractSplitVm>
{
    public int Id { get; set; }
}

public class SupplierContractSplitVmHandler : VmQueryHandler, IRequestHandler<SupplierContractSplitVmQuery, SupplierContractSplitVm>
{
    public SupplierContractSplitVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractSplitVm> Handle(SupplierContractSplitVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractSplits.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierContractSplitId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierContractSplitVm>(entity);
    }
}