namespace Engage.Application.Services.OrderStagingSkus.Queries;

public record OrderStagingSkuVmQuery(int Id) : IRequest<OrderStagingSkuVm>;

public record OrderStagingSkuVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrderStagingSkuVmQuery, OrderStagingSkuVm>
{
    public async Task<OrderStagingSkuVm> Handle(OrderStagingSkuVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.OrderStagingSkus.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.OrderStaging);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.OrderStagingSkuId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<OrderStagingSkuVm>(entity);
    }
}