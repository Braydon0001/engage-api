// auto-generated
namespace Engage.Application.Services.ProductVendors.Queries;

public class ProductVendorVmQuery : IRequest<ProductVendorVm>
{
    public int Id { get; set; }
}

public class ProductVendorVmHandler : VmQueryHandler, IRequestHandler<ProductVendorVmQuery, ProductVendorVm>
{
    public ProductVendorVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductVendorVm> Handle(ProductVendorVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductVendors.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductVendorId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductVendorVm>(entity);
    }
}