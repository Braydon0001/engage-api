namespace Engage.Application.Services.OrderTemplates.Queries;

public class OrderTemplateVmQuery : IRequest<OrderTemplateVm>
{
    public int Id { get; set; }
}

public class OrderTemplateVmHandler : VmQueryHandler, IRequestHandler<OrderTemplateVmQuery, OrderTemplateVm>
{
    public OrderTemplateVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OrderTemplateVm> Handle(OrderTemplateVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.OrderTemplates.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.OrderTemplateStatus)
                             .Include(e => e.DistributionCenter)
                             .Include(e => e.OrderTemplateGroups);

        var entity = await queryable.SingleOrDefaultAsync(e => e.OrderTemplateId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<OrderTemplateVm>(entity);
    }
}