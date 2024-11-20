namespace Engage.Application.Services.OrderStagings.Queries;

public record OrderStagingVmQuery(int Id) : IRequest<OrderStagingVm>;

public record OrderStagingVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrderStagingVmQuery, OrderStagingVm>
{
    public async Task<OrderStagingVm> Handle(OrderStagingVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.OrderStagings.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.OrderStagingId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<OrderStagingVm>(entity);
    }
}