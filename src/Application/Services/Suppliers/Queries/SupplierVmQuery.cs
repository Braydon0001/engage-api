using Engage.Application.Services.Suppliers.Models;

namespace Engage.Application.Services.Suppliers.Queries;

public class SupplierVmQuery : IRequest<SupplierVm>
{
    public int Id { get; set; }
}

public class SupplierVmQueryHandler : BaseQueryHandler, IRequestHandler<SupplierVmQuery, SupplierVm>
{
    public SupplierVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierVm> Handle(SupplierVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Suppliers.Include(x => x.SupplierGroup)
                                             .Include(x => x.SupplierSupplierTypes)
                                             .ThenInclude(x => x.SupplierType)
                                             .Include(x => x.SupplierEngageBrands)
                                             .ThenInclude(x => x.EngageBrand)
                                             .FirstOrDefaultAsync(x => x.SupplierId == request.Id, cancellationToken);

        return _mapper.Map<Supplier, SupplierVm>(entity);
    }
}
