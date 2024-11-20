// auto-generated
namespace Engage.Application.Services.SupplierSubContractTypes.Queries;

public class SupplierSubContractTypeVmQuery : IRequest<SupplierSubContractTypeVm>
{
    public int Id { get; set; }
}

public class SupplierSubContractTypeVmHandler : VmQueryHandler, IRequestHandler<SupplierSubContractTypeVmQuery, SupplierSubContractTypeVm>
{
    public SupplierSubContractTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSubContractTypeVm> Handle(SupplierSubContractTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSubContractTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierSubContractTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierSubContractTypeVm>(entity);
    }
}