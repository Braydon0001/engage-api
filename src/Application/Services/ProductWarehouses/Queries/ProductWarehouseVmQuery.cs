// auto-generated
namespace Engage.Application.Services.ProductWarehouses.Queries;

public class ProductWarehouseVmQuery : IRequest<ProductWarehouseVm>
{
    public int Id { get; set; }
}

public class ProductWarehouseVmHandler : VmQueryHandler, IRequestHandler<ProductWarehouseVmQuery, ProductWarehouseVm>
{
    public ProductWarehouseVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ProductWarehouseVm> Handle(ProductWarehouseVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductWarehouses.AsQueryable().AsNoTracking().Include(e => e.EngageRegion);

        var entity = await queryable
                            .Include(e => e.Parent)
                            .Include(e => e.ProductWarehouseRegions)
                            .ThenInclude(e => e.EngageRegion)
                            .SingleOrDefaultAsync(e => e.ProductWarehouseId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<ProductWarehouseVm>(entity);
    }
}