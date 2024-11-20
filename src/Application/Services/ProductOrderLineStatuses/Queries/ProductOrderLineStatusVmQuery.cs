namespace Engage.Application.Services.ProductOrderLineStatuses.Queries;

public record ProductOrderLineStatusVmQuery(int Id) : IRequest<ProductOrderLineStatusVm>;

public record ProductOrderLineStatusVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineStatusVmQuery, ProductOrderLineStatusVm>
{
    public async Task<ProductOrderLineStatusVm> Handle(ProductOrderLineStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderLineStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductOrderLineStatusId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProductOrderLineStatusVm>(entity);
    }
}