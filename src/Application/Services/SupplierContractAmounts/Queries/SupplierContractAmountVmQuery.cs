// auto-generated
namespace Engage.Application.Services.SupplierContractAmounts.Queries;

public class SupplierContractAmountVmQuery : IRequest<SupplierContractAmountVm>
{
    public int Id { get; set; }
}

public class SupplierContractAmountVmHandler : VmQueryHandler, IRequestHandler<SupplierContractAmountVmQuery, SupplierContractAmountVm>
{
    public SupplierContractAmountVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractAmountVm> Handle(SupplierContractAmountVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractAmounts.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SupplierSubContractDetail)
                             .Include(e => e.SupplierContractAmountType)
                             .Include(e => e.SupplierContractSplit);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierContractAmountId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierContractAmountVm>(entity);
    }
}