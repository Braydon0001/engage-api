// auto-generated
namespace Engage.Application.Services.SupplierContractAmountTypes.Queries;

public class SupplierContractAmountTypeVmQuery : IRequest<SupplierContractAmountTypeVm>
{
    public int Id { get; set; }
}

public class SupplierContractAmountTypeVmHandler : VmQueryHandler, IRequestHandler<SupplierContractAmountTypeVmQuery, SupplierContractAmountTypeVm>
{
    public SupplierContractAmountTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractAmountTypeVm> Handle(SupplierContractAmountTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractAmountTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierContractAmountTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierContractAmountTypeVm>(entity);
    }
}