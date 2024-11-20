namespace Engage.Application.Services.OrderTemplateProducts.Queries;

public class OrderTemplateProductVmQuery : IRequest<OrderTemplateProductVm>
{
    public int Id { get; set; }
}

public class OrderTemplateProductVmHandler : VmQueryHandler, IRequestHandler<OrderTemplateProductVmQuery, OrderTemplateProductVm>
{
    public OrderTemplateProductVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OrderTemplateProductVm> Handle(OrderTemplateProductVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.OrderTemplateProducts.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.OrderTemplateGroup)
                             .Include(e => e.DCProduct)
                             .ThenInclude(e => e.EngageVariantProduct);

        var entity = await queryable.SingleOrDefaultAsync(e => e.OrderTemplateProductId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<OrderTemplateProductVm>(entity);
    }
}