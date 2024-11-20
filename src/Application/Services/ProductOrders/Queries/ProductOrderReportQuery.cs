namespace Engage.Application.Services.ProductOrders.Queries;

public class ProductOrderReportQuery : IRequest<List<ProductOrderReportDto>>
{
    public List<int> ProductWarehouseIds { get; set; }
    public List<int> ProductOrderStatusIds { get; set; }
    public List<int> ProductOrderTypeIds { get; set; }
    public int ProductPeriodId { get; set; }
}
public record ProductOrderReportHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderReportQuery, List<ProductOrderReportDto>>
{
    public async Task<List<ProductOrderReportDto>> Handle(ProductOrderReportQuery query, CancellationToken cancellationToken)
    {
        var warehouseQueryable = Context.ProductWarehouses.AsNoTracking().AsQueryable().Where(e => e.ParentId != null);

        var queryable = Context.ProductOrders.AsQueryable().AsNoTracking();

        if (query.ProductWarehouseIds.Count > 0)
        {
            queryable = queryable.Where(e => query.ProductWarehouseIds.Contains(e.ProductWarehouseId));

            warehouseQueryable = warehouseQueryable.Where(e => query.ProductWarehouseIds.Contains(e.ProductWarehouseId));
        }

        if (query.ProductOrderStatusIds.Count > 0)
        {
            queryable = queryable.Where(e => query.ProductOrderStatusIds.Contains(e.ProductOrderStatusId));
        }

        if (query.ProductOrderTypeIds.Count > 0)
        {
            queryable = queryable.Where(e => query.ProductOrderTypeIds.Contains(e.ProductOrderTypeId));
        }

        var warehouses = await warehouseQueryable.ToListAsync(cancellationToken);

        var orderIds = await queryable
                            .Where(e => e.ProductPeriodId == query.ProductPeriodId)
                            .OrderBy(e => e.ProductOrderId)
                            .Select(e => e.ProductOrderId)
                            .ToListAsync(cancellationToken);

        var lines = await Context.ProductOrderLines
                                    .AsNoTracking()
                                    .Where(e => orderIds.Contains(e.ProductOrderId))
                                    .ProjectTo<ProdutOrderLineReportDto>(Mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);

        var period = await Context.ProductPeriods.FirstOrDefaultAsync(e => e.ProductPeriodId == query.ProductPeriodId, cancellationToken);

        var fileName = $"{period.Name} - {string.Join(", ", warehouses.Select(e => e.Name).ToList()).Truncate(45, "...")}.xlsx";


        List<ProductOrderReportDto> dtoList = [];
        foreach (var warehouse in warehouses)
        {
            dtoList.Add(new ProductOrderReportDto
            {
                ProductWarehouseName = warehouse.Name,
                FileName = fileName,
                Lines = lines.Where(e => e.ProductWarehouseName == warehouse.Name).ToList(),
            });
        }

        return dtoList;
    }
}