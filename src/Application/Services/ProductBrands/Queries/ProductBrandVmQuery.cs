// auto-generated
namespace Engage.Application.Services.ProductBrands.Queries;

public class ProductBrandVmQuery : IRequest<ProductBrandVm>
{
    public int Id { get; set; }
}

public class ProductBrandVmHandler : VmQueryHandler, IRequestHandler<ProductBrandVmQuery, ProductBrandVm>
{
    public ProductBrandVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductBrandVm> Handle(ProductBrandVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductBrands.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductBrandId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductBrandVm>(entity);
    }
}