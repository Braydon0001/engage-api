namespace Engage.Application.Services.ProductOrderLineTypes.Queries;

public record ProductOrderLineTypeVmQuery(int Id) : IRequest<ProductOrderLineTypeVm>;

public record ProductOrderLineTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductOrderLineTypeVmQuery, ProductOrderLineTypeVm>
{
    public async Task<ProductOrderLineTypeVm> Handle(ProductOrderLineTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.ProductOrderLineTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.ProductOrderLineTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<ProductOrderLineTypeVm>(entity);
    }
}