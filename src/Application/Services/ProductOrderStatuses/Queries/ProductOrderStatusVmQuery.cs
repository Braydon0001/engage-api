namespace Engage.Application.Services.ProductOrderStatuses.Queries;

public record ProductOrderStatusVmQuery(int Id) : IRequest<ProductOrderStatusVm>;

public record ProductOrderStatusVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderStatusVmQuery, ProductOrderStatusVm>
{
    public async Task<ProductOrderStatusVm> Handle(ProductOrderStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductOrderStatusId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProductOrderStatusVm>(entity);
    }
}