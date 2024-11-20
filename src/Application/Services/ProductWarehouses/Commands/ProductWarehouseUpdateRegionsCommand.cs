namespace Engage.Application.Services.ProductWarehouses.Commands;

public class ProductWarehouseUpdateRegionsCommand : IRequest<OperationStatus>
{
    public int ProductWarehouseId { get; set; }
    public List<int> EngageRegionIds { get; set; }
}
public class ProductWarehouseUpdateRegionsHandler : BaseUpdateCommandHandler, IRequestHandler<ProductWarehouseUpdateRegionsCommand, OperationStatus>
{
    public ProductWarehouseUpdateRegionsHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(ProductWarehouseUpdateRegionsCommand command, CancellationToken cancellationToken)
    {
        //remove current regions
        var warehouseRegions = await _context.ProductWarehouseRegions
                                             .Where(e => e.ProductWarehouseId == command.ProductWarehouseId)
                                             .ToListAsync(cancellationToken);
        var removedRegions = warehouseRegions.Where(e => !command.EngageRegionIds.Contains(e.EngageRegionId)).ToList();

        _context.ProductWarehouseRegions.RemoveRange(removedRegions);

        command.EngageRegionIds.Where(e => !warehouseRegions.Select(f => f.EngageRegionId).ToList().Contains(e)).ForEach(assignedId =>
        {
            _context.ProductWarehouseRegions.Add(new ProductWarehouseRegion()
            {
                EngageRegionId = assignedId,
                ProductWarehouseId = command.ProductWarehouseId
            });
        });

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
