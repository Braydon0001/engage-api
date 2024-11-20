namespace Engage.Application.Services.ProductOrderLines.Queries;

public record ProductOrderLineVmQuery(int Id) : IRequest<ProductOrderLineVm>;

public record ProductOrderLineVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineVmQuery, ProductOrderLineVm>
{
    public async Task<ProductOrderLineVm> Handle(ProductOrderLineVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderLines.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.ProductOrder)
                             .Include(e => e.Product)
                             .Include(e => e.ProductOrderLineStatus)
                             .Include(e => e.ProductOrderLineType);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductOrderLineId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProductOrderLineVm>(entity);
    }
}