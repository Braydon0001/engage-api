// auto-generated
namespace Engage.Application.Services.ProductSuppliers.Queries;

public class ProductSupplierVmQuery : IRequest<ProductSupplierVm>
{
    public int Id { get; set; }
}

public class ProductSupplierVmHandler : VmQueryHandler, IRequestHandler<ProductSupplierVmQuery, ProductSupplierVm>
{
    public ProductSupplierVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductSupplierVm> Handle(ProductSupplierVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSuppliers.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductSupplierId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductSupplierVm>(entity);
    }
}