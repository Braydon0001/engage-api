// auto-generated
namespace Engage.Application.Services.Products.Queries;

public class ProductVmQuery : IRequest<ProductVm>
{
    public int Id { get; set; }
}

public class ProductVmHandler : VmQueryHandler, IRequestHandler<ProductVmQuery, ProductVm>
{
    public ProductVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductVm> Handle(ProductVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.Products.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProductMaster)
                             .Include(e => e.RelatedProduct)
                             .Include(e => e.ProductWarehouse)
                             .Include(e => e.ProductSizeType)
                             .Include(e => e.ProductPackSizeType)
                             .Include(e => e.ProductModuleStatus)
                             .Include(e => e.ProductSystemStatus)
                             .Include(e => e.ProductMasterColor)
                             .ThenInclude(e => e.ProductMaster)
                             .Include(e => e.ProductMasterSize)
                             .ThenInclude(e => e.ProductMaster);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductVm>(entity);
    }
}