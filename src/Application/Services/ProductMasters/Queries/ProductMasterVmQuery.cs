// auto-generated
namespace Engage.Application.Services.ProductMasters.Queries;

public class ProductMasterVmQuery : IRequest<ProductMasterVm>
{
    public int Id { get; set; }
}

public class ProductMasterVmHandler : VmQueryHandler, IRequestHandler<ProductMasterVmQuery, ProductMasterVm>
{
    public ProductMasterVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductMasterVm> Handle(ProductMasterVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasters.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProductBrand)
                             .Include(e => e.ProductReason)
                             .Include(e => e.ProductSubCategory)
                             .Include(e => e.ProductMasterStatus)
                             .Include(e => e.ProductMasterSystemStatus)
                             .Include(e => e.ProductVendor)
                             .Include(e => e.ProductManufacturer);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductMasterId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductMasterVm>(entity);
    }
}