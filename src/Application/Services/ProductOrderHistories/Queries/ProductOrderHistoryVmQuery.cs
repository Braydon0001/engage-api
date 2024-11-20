namespace Engage.Application.Services.ProductOrderHistories.Queries;

public record ProductOrderHistoryVmQuery(int Id) : IRequest<ProductOrderHistoryVm>;

public record ProductOrderHistoryVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderHistoryVmQuery, ProductOrderHistoryVm>
{
    public async Task<ProductOrderHistoryVm> Handle(ProductOrderHistoryVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderHistories.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProductOrder)
                             .Include(e => e.ProductOrderStatus);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductOrderHistoryId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProductOrderHistoryVm>(entity);
    }
}