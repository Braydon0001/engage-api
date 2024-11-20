// auto-generated
namespace Engage.Application.Services.SupplierContractDetailTypes.Queries;

public class SupplierContractDetailTypeVmQuery : IRequest<SupplierContractDetailTypeVm>
{
    public int Id { get; set; }
}

public class SupplierContractDetailTypeVmHandler : VmQueryHandler, IRequestHandler<SupplierContractDetailTypeVmQuery, SupplierContractDetailTypeVm>
{
    public SupplierContractDetailTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierContractDetailTypeVm> Handle(SupplierContractDetailTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierContractDetailTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierContractDetailTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierContractDetailTypeVm>(entity);
    }
}