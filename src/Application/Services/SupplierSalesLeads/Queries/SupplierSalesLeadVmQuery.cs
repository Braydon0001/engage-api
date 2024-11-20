// auto-generated
namespace Engage.Application.Services.SupplierSalesLeads.Queries;

public class SupplierSalesLeadVmQuery : IRequest<SupplierSalesLeadVm>
{
    public int Id { get; set; }
}

public class SupplierSalesLeadVmHandler : VmQueryHandler, IRequestHandler<SupplierSalesLeadVmQuery, SupplierSalesLeadVm>
{
    public SupplierSalesLeadVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierSalesLeadVm> Handle(SupplierSalesLeadVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierSalesLeads.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierSalesLeadId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierSalesLeadVm>(entity);
    }
}