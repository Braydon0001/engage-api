// auto-generated
namespace Engage.Application.Services.ProductWarehouses.Queries;

public class ProductWarehouseListQuery : IRequest<List<ProductWarehouseDto>>
{

}

public class ProductWarehouseListHandler : ListQueryHandler, IRequestHandler<ProductWarehouseListQuery, List<ProductWarehouseDto>>
{
    public ProductWarehouseListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductWarehouseDto>> Handle(ProductWarehouseListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductWarehouses.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
                              .Include(e => e.Parent)
                              .Include(e => e.ProductWarehouseRegions)
                              .ThenInclude(e => e.EngageRegion)
                              .ProjectTo<ProductWarehouseDto>(_mapper.ConfigurationProvider)
                              .OrderBy(e => e.Id)
                              .ToListAsync(cancellationToken);
    }
}