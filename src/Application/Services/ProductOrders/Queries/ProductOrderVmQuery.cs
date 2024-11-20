namespace Engage.Application.Services.ProductOrders.Queries;

public record ProductOrderVmQuery(int Id) : IRequest<ProductOrderVm>;

public record ProductOrderVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderVmQuery, ProductOrderVm>
{
    public async Task<ProductOrderVm> Handle(ProductOrderVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrders.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProductOrderStatus)
                             .Include(e => e.ProductWarehouse)
                             .Include(e => e.ProductWarehouseOut)
                             .Include(e => e.ProductOrderType)
                             .Include(e => e.ProductPeriod)
                             .Include(e => e.ProductSupplier);

        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductOrderId == query.Id, cancellationToken);

        var mappedEntity = entity == null ? null : Mapper.Map<ProductOrderVm>(entity);

        var orderLines = await Context.ProductOrderLines.AsNoTracking()
                                                        .Where(e => e.ProductOrderId == query.Id)
                                                        .ToListAsync(cancellationToken);

        mappedEntity.OrderLineCount = orderLines.Count;

        if (mappedEntity != null)
        {
            if (mappedEntity.ProductOrderStatusId.Id == (int)ProductOrderStatusEnum.Rejected)
            {
                var rejectReason = await Context.ProductOrderHistories.Where(e => e.ProductOrderId == query.Id
                                                                            && e.ProductOrderStatusId == (int)ProductOrderStatusEnum.Rejected)
                                                                      .FirstOrDefaultAsync(cancellationToken);

                mappedEntity.RejectReason = rejectReason == null ? "" : rejectReason.Reason;

            }
        }

        return mappedEntity;
    }
}