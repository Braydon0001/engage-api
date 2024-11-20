// auto-generated
namespace Engage.Application.Services.SupplierContractTypes.Queries;

public class SupplierContractTypeVmQuery : IRequest<SupplierContractTypeVm>
{
    public int Id { get; set; }
}

public class SupplierContractTypeVmHandler : VmQueryHandler, IRequestHandler<SupplierContractTypeVmQuery, SupplierContractTypeVm>
{
    public SupplierContractTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractTypeVm> Handle(SupplierContractTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierContractTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierContractTypeVm>(entity);
    }
}