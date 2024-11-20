// auto-generated
namespace Engage.Application.Services.ProductManufacturers.Queries;

public class ProductManufacturerVmQuery : IRequest<ProductManufacturerVm>
{
    public int Id { get; set; }
}

public class ProductManufacturerVmHandler : VmQueryHandler, IRequestHandler<ProductManufacturerVmQuery, ProductManufacturerVm>
{
    public ProductManufacturerVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductManufacturerVm> Handle(ProductManufacturerVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductManufacturers.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProductSupplier);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductManufacturerId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductManufacturerVm>(entity);
    }
}